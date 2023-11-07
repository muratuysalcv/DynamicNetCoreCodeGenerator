using System;
using System.Collections.Generic;
using System.Threading;
using System.Web;
using System.Data.Sql;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Scriptingo.Admin;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Scriptingo.Admin
{
    public static class HtmlExtension
    {
        public static IHtmlContent T(this IHtmlHelper htmlHelper, string word, params string[] p)
        {
            LocalizationManager localizationManager = new LocalizationManager();
            var response = localizationManager.GetResource(word, Thread.CurrentThread.CurrentCulture.Name);
            for (int i = 0; i < p.Length; i++)
            {
                response = response.Replace("{" + i + "}", p[i]);
            }

            return new HtmlString(response);
        }
    }
}