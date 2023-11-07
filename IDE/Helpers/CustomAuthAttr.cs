using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Scriptingo.Admin.Managers;
using System.Diagnostics.CodeAnalysis;
using System.Security.Authentication;

namespace Scriptingo.Admin.Helpers
{
    public class FastAuthorizeAttribute : AuthorizeAttribute
    {
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return base.Equals(obj);
        }
        public override bool Match(object? obj)
        {
            return base.Match(obj);
        }
    }
    public class ApiActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"] + "";
            var actionName = context.RouteData.Values["action"] + "";
            var user = SessionManager.GetUser(context.HttpContext);
            // Do something before the action executes.
            if(user == null && controllerName.ToLower()=="execute")
            {
                throw new AuthenticationException();
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Do something after the action executes.

        }
    }
}
