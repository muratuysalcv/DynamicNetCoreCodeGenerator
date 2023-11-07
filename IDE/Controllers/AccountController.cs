using CaptchaNoJS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Scriptingo.Admin.Managers;
using Scriptingo.Admin.Models;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Scriptingo.Admin.Controllers
{
    public class AccountController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var dbUser = new FastApiContext<_user>();

            if (ModelState.IsValid)
            {
                var u = dbUser.Data.FirstOrDefault(x => x.e_mail == model.Email && x.password == model.Password);
                if (u == null)
                {
                    ModelState.AddModelError("", T("Please check your credentials."));
                }
                //else if (u.status == 0)
                //{
                //    ModelState.AddModelError("", T("Please approve your account with sent email."));
                //}
                else if (u.status < 0)
                {
                    ModelState.AddModelError("", T("Your account is locked or disabled. Please contact administrator."));
                }
                else
                {
                    AddSuccessMessage(T("Login successfully completed."));
                    SessionManager.SetSession(u.ID, HttpContext);

                    return RedirectToAction("index", "home");
                }
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterModel model)
        {
            var dbUser = new FastApiContext<_user>();
            if (dbUser.Data.Count(x => x.e_mail == model.Email) > 0)
            {
                ModelState.AddModelError("Email", T("Email already using. You can use password reset."));
            }
            else if (!model.CheckedPolicy)
            {
                ModelState.AddModelError("CheckedPolicy", T("Please accept policy rules."));
            }
            else if (ModelState.IsValid)
            {
                var temp_code = (new Random()).Next(500000, 999999) + "";
                var user = new _user()
                {
                    e_mail = model.Email,
                    first_name = model.FirstName,
                    last_name = model.LastName,
                    password = model.Password,
                    status = 0,
                    wrong_try = 0,
                    temp_code = temp_code,
                    uid = Guid.NewGuid()
                };
                dbUser.Add(user);
                dbUser.SaveChanges();
                SessionManager.SetSession(user.ID, HttpContext);
                EmailSender.SendEmail(model.Email, "Latest Api - Validation Code : " + temp_code, "Your validation code is : " + temp_code);
                return RedirectToAction("Validation");
            }
            return View(model);
        }

        public IActionResult ActionRequiredMode()
        {
            var user = SessionManager.GetUser(HttpContext);
            var model = new ActionRequiredMode()
            {
                Email = user.e_mail,
                FirstName = user.first_name,
                LastName = user.last_name,
                Status = user.status
            };
            if (user.status < 0)
            {
                model.Message = T("Please contact administration on info@latestapi.com.");
            }
            return View(model);
        }

        public IActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            Captcha captcha = new Captcha(300, 100, 4);
            model.CaptchaBase64 = captcha.GenerateAsB64(Captcha.CaptchaType.Line); // or captcha.GenerateAsB64(Captcha.CaptchaType.Circle);
            HttpContext.Session.SetString("captchaAnswer", captcha.GetAnswer());

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(ForgotPasswordModel model)
        {
            var dbUser = new FastApiContext<_user>();
            if (model.CaptchaText != HttpContext.Session.GetString("captchaAnswer"))
            {
                ModelState.AddModelError("CaptchaText", T("Please check your informations."));
            }
            else if (dbUser.Data.Count(x => x.e_mail == model.Email) == 0)
            {
                ModelState.AddModelError("Email", T("Please check your informations."));
            }
            if (ModelState.IsValid)
            {
                var user = dbUser.Data.FirstOrDefault(x => x.e_mail == model.Email);
                if (user.status < 1)
                {
                    ModelState.AddModelError("Email", T("Your account locked or disabled. Please contact on info@lastestapi.com."));
                }
                else
                {
                    var code = (Guid.NewGuid()).ToString().Replace("-", "");
                    user.temp_code = code + "";
                    dbUser.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbUser.SaveChanges();
                    string link = this.DomainUrl("?ResetCode=" + code + "&Email=" + user.e_mail);
                    EmailSender.SendEmail(user.e_mail, T("Password Recorvery"), T("Your pasword reset code is : {0} </br><br> <a href='{1}'>Reset Link</a> ", code + "", link));
                    AddSuccessMessage(T("Your password reset code sent to your email address."));
                    return RedirectToAction("Index", "Home");
                }
            }
            Captcha captcha = new Captcha(300, 100, 4);
            model.CaptchaBase64 = captcha.GenerateAsB64(Captcha.CaptchaType.Circle); // or captcha.GenerateAsB64(Captcha.CaptchaType.Circle);
            HttpContext.Session.SetString("captchaAnswer", captcha.GetAnswer());

            return View(model);
        }

        public IActionResult PasswordReset(string resetCode, string Email)
        {
            var model = new PasswordResetModel()
            {
                Email = Email,
                ResetCode = resetCode
            };

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasswordReset(PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                var dbUser = new FastApiContext<_user>();
                var user = dbUser.Data.FirstOrDefault(x => x.e_mail == model.Email);
                if (user == null)
                    ModelState.AddModelError("Email", T("Please check your information."));
                else if (user.status == 0)
                    ModelState.AddModelError("Email", T("Your account is not approved.Please check your email adress."));
                else if (user.status < 0)
                    ModelState.AddModelError("Email", T("Your account is locked. Please contact administration."));
                else if (user.temp_code != model.ResetCode)
                    ModelState.AddModelError("Email", T("Reset code is not valid. Please check your informations."));
                else if (ModelState.IsValid)
                {
                    user.password = model.NewPassword;
                    user.temp_code = (Guid.NewGuid() + "").Replace("-", "");
                    dbUser.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    dbUser.SaveChanges();
                    AddSuccessMessage(T("Your password reset completed successfully. You can login with new password."));
                    return RedirectToAction("Login");
                }
            }

            return View(model);
        }

        public IActionResult Logout()
        {
            SessionManager.Logout(this.HttpContext);
            AddSuccessMessage(T("Your logout successfully completed."));
            return RedirectToAction("index", "home");
        }
    }
}
