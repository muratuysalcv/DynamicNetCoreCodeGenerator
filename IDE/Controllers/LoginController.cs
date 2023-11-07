using Microsoft.AspNetCore.Mvc;
using Scriptingo.Admin.Managers;

namespace Scriptingo.Admin.Controllers
{
    public class LoginController: Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
