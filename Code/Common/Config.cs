using Newtonsoft.Json;
using System.IO;
using System.Text;
using System;
using Scriptingo.FastApi.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Common
{
    public class Config
    {
        public string DefaultSchema { get; set; }
        public string DeployPath { get; set; }
        public string IDEProjectDir { get; set; }
        public string ApiProjectDir { get; set; }
        public string ApiContactName { get; set; }
        public string ApiContactEmail { get; set; }
        public string CommonProjectDir { get; set; }
        public string CodeDir { get; set; }
        public string RegisterToken { get; set; }
        public string BuildDir { get; set; }

        public static string PostgreSql = "postgre";
        public static string MsSql = "mssql";
        public static string Oracle = "oracle";
        public static string MySql = "mysql";
        public static string Sqlite = "sqlite";
        public string MainDbType { get; set; }
        public string MainDbConnectionString { get; set; }
        public List<DbConnection> FastApiConnections { get; set; }

        public static Config Get(string baseDirectory = null)
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
            return JsonConvert.DeserializeObject<Config>(content);
        }
        public void Save(string baseDirectory)
        {
            string fileContent = JsonConvert.SerializeObject(this);
            File.WriteAllText(baseDirectory+"\\appsettings.json", fileContent);
        }
        public Jwt Jwt { get; set; }
    }

    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
    }
    public class DbConnection
    {
        public string name { get; set; }
        public string dbType { get; set; }
        public string schema { get; set; }
        public string connectionString{ get; set; }
    }
}
