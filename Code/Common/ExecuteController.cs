using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Scriptingo.Common;
using Scriptingo.Common.Models;
using Scriptingo.FastApi.Common;
using System.Net;

namespace Scriptingo.FastApi.Controllers
{
    /// <summary>
    /// Dynamically runs your process. 
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ExecuteController : ControllerBase
    {
        /// <summary>
        /// ExecuteNonQuery function runs on process. If you dont return any data
        /// you must use.
        /// </summary>
        /// <param name="p">ProcessName and Process Parameters info object.</param>
        /// <returns>ApiReturn returns and if status='success' process successfull. If error detail in message field.</returns>
        [HttpPost(Name = "Process")]
        public ApiReturn Process([FromBody] ProcessRequest p)
        {
            var ret = new ApiReturn()
            {
                status = "success"
            };
            var db = new FastApiContext<_process>();
            var process = db.Data.FirstOrDefault(x => x.name == p.ProcessName);
            try
            {
                db.ExecuteSql(process.sql, p.Parameters);
            }
            catch (Exception ex)
            {
                ret.status = "error";
                ret.message = ex.Message;
            }
            return ret;
        }

        /// <summary>
        /// If your process return data
        /// you must use. If query successful, you can find data in 'data' field. 
        /// </summary>
        /// <param name="p">ProcessName and Process Parameters info object.</param>
        /// <returns>ApiReturn returns and if status='success' process successfull. If error detail in message field.</returns>

        [HttpPost(Name = "DataProcess")]
        public ContentResult DataProcess([FromBody] ProcessRequest p)
        {
            var ret = new ApiReturn()
            {
                status = "success"
            };
            var db = new FastApiContext<_process>();
            var process = db.Data.FirstOrDefault(x => x.name == p.ProcessName);
            string realDataText = "";
            try
            {
                var dt = db.ExecuteDataSql(process.sql, p.Parameters);
                realDataText = JsonConvert.SerializeObject(dt);
                ret.data = "<<JSON_DATA>>";
            }
            catch (Exception ex)
            {
                ret.status = "error";
                ret.message = ex.Message;
            }
            var retString = JsonConvert.SerializeObject(ret);
            if (!string.IsNullOrEmpty(realDataText))
                retString = retString.Replace(@"""<<JSON_DATA>>""", realDataText);

            return Content(retString, "application/json");
        }
    }
}