using Microsoft.AspNetCore.Mvc;
using Scriptingo.Common.Models;
using Scriptingo.Common;
using System.Globalization;
using Scriptingo.Admin.Managers;

namespace Scriptingo.Admin.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            SessionManager.SetSession(1, this.HttpContext);
            return RedirectToAction("manage", "api", new { id = 123456 });
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            var language = new FastApiContext<_lang>().Data.ToList().FirstOrDefault(x => x.culture_code == culture);
            if (language != null)
            {
                HttpContext.Response.Cookies.Append("language", culture, new CookieOptions()
                {
                    Expires = DateTime.MaxValue
                });
                Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
                CultureInfo.CurrentCulture = new System.Globalization.CultureInfo(culture);
                CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            }
            return Redirect(returnUrl);
        }

    }
}
