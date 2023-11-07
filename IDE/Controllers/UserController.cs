using Microsoft.AspNetCore.Mvc;
using Scriptingo.Admin.Managers;
using Scriptingo.Admin.Models;
using Scriptingo.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Admin.Controllers
{
    public class UserController : BaseLoginedController
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Validation()
        {
            return View();
        }
        [HttpPost()]
        public IActionResult Validation(ValidationModel model)
        {
            var user = SessionManager.GetUser(HttpContext);
            if (user.status != 0)
            {
                ModelState.AddModelError("ValidationCode", T("Your status not available to validate your account."));
            }
            else if (user.temp_code != model.ValidationCode)
            {
                ModelState.AddModelError("ValidationCode", T("Your code is not valid. Please check your email."));
            }
            if (ModelState.IsValid)
            {
                var dbUser = new FastApiContext<_user>();
                user = dbUser.Data.FirstOrDefault(x => x.ID == user.ID);
                user.status = 1;
                dbUser.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dbUser.SaveChanges();
                AddSuccessMessage(T("Your account successfully activated."));
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                AddErrorMessage(T("Please check your informations."));
            }
            return View(model);
        }
    }
}
