namespace Scriptingo.Admin.Helpers
{
    public class HttpHelper
    {
        public string ApiUrl { get; set; }
        public HttpHelper() {
            var config = AdminConfig.Get();
            this.ApiUrl=config.ApiUrl;
        }


    }
}
