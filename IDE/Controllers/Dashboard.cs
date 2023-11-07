using Microsoft.AspNetCore.Mvc;

namespace Scriptingo.Admin.Controllers
{
    public class Dashboard : BaseLoginedController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
