using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.OpenApi.Extensions;
using Scriptingo.Common.Models;

namespace Scriptingo.Common
{
    public class ModelConfiguration
    {
        public string DbType { get; set; }
        public string Schema { get; set; }
        public string ConnectionName { get; set; }

        public string ConnectionString { get; set; }
        public ModelConfiguration() { }
        public void SetConfigurationByType<T>()
        {
            var type = typeof(T);
            var fastApiAttr = type.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "FastApiTableAttribute");
            var tableAttr = type.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == "TableAttribute");

            this.ConnectionName = fastApiAttr.ConstructorArguments[0].Value + "";
            this.DbType = fastApiAttr.ConstructorArguments[1].Value + "";
            this.Schema = tableAttr.NamedArguments[0].TypedValue.Value + "";
            var config = Config.Get();
            if (!string.IsNullOrEmpty(this.ConnectionName))
            {
                var dbConfig = config.FastApiConnections.FirstOrDefault(x => x.name == this.ConnectionName);
                this.ConnectionString = dbConfig.connectionString;
            }
            else
            {
                this.ConnectionString = config.MainDbConnectionString;
            }
        }

        public ModelConfiguration(_con con)
        {
            var dbConType = new FastApiContext<_con>();
            var conType = dbConType.Data.FirstOrDefault(x => x.ID == con.con_type_id);
            this.ConnectionName = con.name;
            this.DbType = conType.name;
            this.Schema = con.db_schema;
            this.ConnectionString = con.ConStr();
        }
        public ModelConfiguration(string appSettingsKey)
        {
            var config = Config.Get();
            if (appSettingsKey + "" == "Main")
            {
                this.DbType = config.MainDbType;
                this.ConnectionString = config.MainDbConnectionString;
                this.Schema = config.DefaultSchema;
            }
            else
            {
                var dbConfiG = config.FastApiConnections.FirstOrDefault(x => x.name == appSettingsKey);
                this.ConnectionName = dbConfiG.name;
                this.DbType = dbConfiG.dbType;
                this.Schema = dbConfiG.schema;
                this.ConnectionString = dbConfiG.connectionString;
            }
        }

    }

}


