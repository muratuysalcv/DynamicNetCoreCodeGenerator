using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using MySql.Data.MySqlClient;
using Npgsql;
using Scriptingo.Common;
using Scriptingo.FastApi;
using Scriptingo.FastApi.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    public class FastApiSqlExecuterContext 
    {

        public ModelConfiguration Configuration { get; set; }

        public FastApiSqlExecuterContext(ModelConfiguration conf)
        {
            this.Configuration = conf;
        }

        public DataTable ExecuteDataSql(string sql, List<data> parameters)
        {
            var list = new List<DataTable>();
            DbDataReader reader = null;
            SqlConnection sqlCon = null;
            MySqlConnection mySqlCon= null;
            SqliteConnection sqliteCon = null;
            if (Configuration.DbType == Config.PostgreSql)
            {
                var dataSource = NpgsqlDataSource.Create(Configuration.ConnectionString);
                var command = dataSource.CreateCommand(sql);
                reader = command.ExecuteReader();
            }
            else if (Configuration.DbType == Config.MySql)
            {
                mySqlCon = new MySqlConnection(Configuration.ConnectionString);
                var command = new MySqlCommand(sql,mySqlCon);
                mySqlCon.Open();
                reader = command.ExecuteReader();
            }
            else if (Configuration.DbType == Config.Sqlite)
            {
                sqliteCon = new SqliteConnection(Configuration.ConnectionString);
                var command = new SqliteCommand(sql, sqliteCon);
                sqliteCon.Open();
                reader = command.ExecuteReader();
            }
            else if (Configuration.DbType == Config.MsSql)
            {
                sqlCon = new SqlConnection(Configuration.ConnectionString);
                var command = new SqlCommand(sql, sqlCon);
                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        var info = (parameter.info + "").ToLower();
                        var dbType = System.Data.SqlDbType.Int;
                        if (info == "int")
                            dbType = System.Data.SqlDbType.Int;
                        else if (info == "decimal")
                            dbType = System.Data.SqlDbType.Decimal;
                        else if (info == "string")
                            dbType = System.Data.SqlDbType.NVarChar;
                        else if (info == "float")
                            dbType = System.Data.SqlDbType.Float;
                        else if (info == "text")
                            dbType = System.Data.SqlDbType.Text;

                        command.Parameters.Add(new SqlParameter()
                        {
                            SqlDbType = dbType,
                            Value = parameter.value,
                            ParameterName = parameter.name
                        });
                    }
                }
                sqlCon.Open();
                reader = command.ExecuteReader();
            }


            var r = reader.HasRows;
            var dt = new DataTable();
            dt.Load(reader);

            if (sqlCon != null) { sqlCon.Close(); }
            else if (mySqlCon != null) { mySqlCon.Close(); }
            else if (sqliteCon != null) { sqliteCon.Close(); }

            return dt;
        }

        public void ExecuteSql(string sql, List<data> parameters)
        {
            if (Configuration.DbType == Config.MsSql)
            {
                var sqlCon = new SqlConnection(Configuration.ConnectionString);
                var cmd = new SqlCommand(sql, sqlCon);
                if (parameters != null)
                {
                    foreach (var p in parameters)
                    {
                        var info = (p.info + "").ToLower();
                        var dbType = SqlDbType.NVarChar;
                        if (info == "int")
                            dbType = SqlDbType.Int;
                        else if (info == "decimal")
                            dbType = SqlDbType.Decimal;
                        else if (info == "string")
                            dbType = SqlDbType.NVarChar;
                        else if (info == "float")
                            dbType = SqlDbType.Float;
                        else if (info == "text")
                            dbType = SqlDbType.Text;

                        cmd.Parameters.Add(new SqlParameter()
                        {
                            SqlDbType = dbType,
                            ParameterName = p.name,
                            Value = p.value == null ? DBNull.Value : p.value,
                        });
                    }
                }
                sqlCon.Open();
                cmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            else
            if (Configuration.DbType == Config.PostgreSql)
            {

            }
            else if (Configuration.DbType == Config.Oracle)
            {

            }
            else if (Configuration.DbType == Config.MySql)
            {

            }
        }

    }
}
