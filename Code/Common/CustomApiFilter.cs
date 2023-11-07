using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    public class CustomApiActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"] + "";
            var actionName = context.RouteData.Values["action"] + "";
            bool tokenValid = true;
            

            
            if (!tokenValid)
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
