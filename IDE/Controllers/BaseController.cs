using Azure;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Scriptingo.Admin.Managers;
using Scriptingo.Common.Models;
using Scriptingo.FastApi.Common;
using System.Collections.Generic;
using System.Globalization;

namespace Scriptingo.Admin.Controllers
{
    public class BaseController : Controller
    {
        private LocalizationManager t { get; set; }
        public BaseController()
        {
            t = new LocalizationManager();
        }
        public string T(string text,params string[] p)
        {
            var res = t.GetResource(text, Thread.CurrentThread.CurrentCulture.Name);
            if(p.Length>0)
            {
                int i = 0;
                foreach(var item in p)
                {
                    res=res.Replace("{"+i+"}", item);
                    i++;
                }
            }
            return res;
        }
        public string DomainUrl(string suffix="")
        {
            var adminConfig = AdminConfig.Get();
            string url = "http://";
            if (adminConfig.SslEnabled)
                url = "https://";
            url += adminConfig.Domain;

            if (!string.IsNullOrEmpty(suffix))
                url += suffix;

            return url;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var cultures = new List<string> { "tr-TR", "en-US" };
            var culture = context.RouteData.Values["culture"] + "";
            var currentLang = context.HttpContext.Request.Cookies["language"] + "";
            if (string.IsNullOrEmpty(currentLang))
            {
                currentLang = "en-US";
                context.HttpContext.Response.Cookies.Append("language", currentLang, new CookieOptions()
                {
                    Expires = DateTime.MaxValue
                });
            }
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(currentLang);
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(currentLang);
            CultureInfo.CurrentCulture = new System.Globalization.CultureInfo(currentLang);
            CultureInfo.CurrentUICulture = new System.Globalization.CultureInfo(currentLang);

            ViewData["RawUrl"] = context.HttpContext.Request.Path;
            ViewData["Culture"] = currentLang;
            base.OnActionExecuting(context);
        }

        public List<data> Messages()
        {
            var list = SessionManager.GetObject<List<data>>("messages",HttpContext);
            if (list == null)
                list = new List<data>();

            return list;
        }
        private void AddMessage(string msg, string key)
        {
            var list = SessionManager.GetObject<List<data>>("messages", HttpContext);
            if (list == null)
                list = new List<data>();
            list.Add(new data() { name = key, value = msg });
            SessionManager.SetObject<List<data>>(list, "messages", HttpContext);
        }
        public void AddSuccessMessage(string message)
        {
            AddMessage(message, "success");
        }
        public void AddErrorMessage(string message)
        {
            AddMessage(message, "error");
        }
        public void AddWarningMessage(string message)
        {
            AddMessage(message, "warning");
        }

        public _user GetUser()
        {
            return SessionManager.GetUser(HttpContext);
        }
        public long? GetUserId()
        {
            return SessionManager.GetUserId(HttpContext);
        }
    }
}
