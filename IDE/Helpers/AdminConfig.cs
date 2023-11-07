using Newtonsoft.Json;
using System.IO;
using System.Text;
using System;
using Scriptingo.FastApi.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Admin
{
    public class AdminConfig
    {
        public string DefaultSchema { get; set; }
        public string DeployPath { get; set; }
        public string IDEProjectDir { get; set; }
        public string ApiProjectDir { get; set; }
        public int ApiCodeDigit { get; set; }
        public string ApiContactName { get; set; }
        public string ApiContactEmail { get; set; }
        public string CommonProjectDir { get; set; }
        public string MainDbType { get; set; }
        public string MainDbConnectionString { get; set; }
        public string Domain { get; set; }
        public bool SslEnabled { get; set; }
        public string ApiUrl { get; set; }

        public static AdminConfig Get(string baseDirectory = null)
        {
            var filePath = AppDomain.CurrentDomain.BaseDirectory + "appsettings.json";
            if (!string.IsNullOrEmpty(baseDirectory))
                filePath = baseDirectory + "appsettings.json";
                string content = "";
            var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                content = streamReader.ReadToEnd();
                fileStream.Close();
            }
            return JsonConvert.DeserializeObject<AdminConfig>(content);
        }
        public void Save(string baseDirectory)
        {
            string fileContent = JsonConvert.SerializeObject(this);
            File.WriteAllText(baseDirectory+"\\appsettings.json", fileContent);
        }
        public Jwt Jwt { get; set; }
        public MailSettings MailSettings { get; set; }
    }

    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
    public class MailSettings
    {
        public string Email { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Password { get; set; }
    }
}
