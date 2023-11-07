using Azure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scriptingo.Admin.Managers;

namespace Scriptingo.Admin.Controllers
{
    public class BaseLoginedController : BaseController
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            
            SessionManager.SetSession(1, context.HttpContext);


            if (!SessionManager.IsLogined(context.HttpContext))
            {
                context.HttpContext.Response.Redirect(Url.Action("Login", "Account", new { returnUrl = context.HttpContext.Request.Path }));
                return Task.CompletedTask;
            }
            else
            {
                var logined = SessionManager.GetUser(context.HttpContext);
                if (logined.status == 0 && controllerName!= "User" && actionName!= "Validation")
                {
                    context.HttpContext.Response.Redirect(Url.Action("Validation", "User"));
                    return Task.CompletedTask;
                }
                else if (logined.status < 0)
                {
                    context.HttpContext.Response.Redirect(Url.Action("ActionRequiredMode", "Account"));
                    return Task.CompletedTask;
                }
            }
            return base.OnActionExecutionAsync(context, next);
        }
    }
}
