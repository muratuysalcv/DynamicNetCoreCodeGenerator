using Azure;
using Newtonsoft.Json;
using Scriptingo.FastApi.Common;
using System.Data;

namespace Scriptingo.Common
{
    /// <summary>
    /// Return objesidir.
    /// </summary>
    public class ApiReturn
    {
        /// <summary>
        /// durum bilgisini verir. 
        /// eğer 'success' ise sorun yoktur. hata varsa
        /// message içerisinde hata detayı yer alır. 
        /// </summary>
        public string status { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public DataTable dt { get; set; }
        public List<data> Errors { get; set; }
        public List<data> Logs { get; set; }

        public ApiReturn() {
            this.status = "success";
        }

        public string jsonFromDatatable(DataTable dt) {
            this.data = "<<JSON_DATA_REPLACE_AREA>>";
            var dataJson = JsonConvert.SerializeObject(dt);
            var returnJson = JsonConvert.SerializeObject(this);
            returnJson = returnJson.Replace("\"<<JSON_DATA_REPLACE_AREA>>\"", dataJson);
            return returnJson;
        }
    }
}
