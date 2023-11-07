using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Scriptingo.Common;

namespace Scriptingo.Common
{
    /// <summary>
    /// 
    /// </summary>
    /// 
    [Authorize]
    public class BaseProcessController : Controller
    {
        public string SQL { get; set; }
        public FastApiSqlExecuterContext sqlExecuter { get; set; }
        public BaseProcessController()
        {
            sqlExecuter = new FastApiSqlExecuterContext(new ModelConfiguration(""));
        }



    }
}