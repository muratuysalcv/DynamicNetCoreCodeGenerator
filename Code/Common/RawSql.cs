using Scriptingo.FastApi.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    public class RawSqlExecuter
    {
        public string Sql { get; set; }
        public List<data> Parameters { get; set; }
        public ModelConfiguration Configuration { get; set; }
        public RawSqlExecuter(string sql, List<data> parameters, ModelConfiguration conf)
        {
            this.Sql = sql;
            this.Parameters = parameters;
            this.Configuration = conf;
        }
        public string BuildSql()
        {
            string sql = "";
            if (Configuration.DbType == Config.MsSql || Configuration.DbType == Config.MySql)
            {
                sql = this.Sql.Replace("{{", "@").Replace("}}", "");
            }
            else if (Configuration.DbType == Config.Oracle)
            {
                sql = this.Sql.Replace("{{", ":").Replace("}}", "");
            }
            return sql;
        }
        public DataTable Execute()
        {
            var dbExecuter = new FastApiSqlExecuterContext(Configuration);
            return dbExecuter.ExecuteDataSql(this.BuildSql(), Parameters);
        }
    }
}
