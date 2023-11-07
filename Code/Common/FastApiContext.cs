using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;
using Npgsql;
using Scriptingo.Common;
using Scriptingo.Common.Models;
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
    public class FastApiContext<T> : DbContext where T : BaseModel
    {
        public DbSet<T> Data { get; set; }

        public ModelConfiguration Configuration { get; set; }

        public FastApiContext()
        {
            this.Configuration = new ModelConfiguration();
            this.Configuration.SetConfigurationByType<T>();
        }
        public FastApiContext(_con con)
        {
            this.Configuration = new ModelConfiguration(con);
        }
        public FastApiContext(string appSettingsKey)
        {
            this.Configuration = new ModelConfiguration(appSettingsKey);
        }

        public DataTable ExecuteDataSql(string sql, List<data> parameters)
        {
            var list = new List<DataTable>();
            DbDataReader reader = null;
            SqlConnection sqlCon = null;
            if (Configuration.DbType == Config.PostgreSql)
            {
                var dataSource = NpgsqlDataSource.Create(Configuration.ConnectionString);
                var command = dataSource.CreateCommand(sql);
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           
            if (Configuration.DbType == Config.MsSql)
            {
                optionsBuilder.UseSqlServer(this.Configuration.ConnectionString);
            }
            else if (Configuration.DbType == Config.PostgreSql)
            {
                optionsBuilder.UseNpgsql(this.Configuration.ConnectionString);
            }
            else if (Configuration.DbType == Config.Oracle)
            {
                optionsBuilder.UseOracle(this.Configuration.ConnectionString);
            }
            else if (Configuration.DbType == Config.MySql)
            {
                optionsBuilder.UseMySQL(this.Configuration.ConnectionString, options => { });
            }
            else if (Configuration.DbType == Config.Sqlite)
            {
                optionsBuilder.UseSqlite(this.Configuration.ConnectionString, options => { });
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Type T is added as a DbSet to the context, but without a Key. 
            // A key is not required as this will be used only for data 
            // retrieval and with AsNoTracking
            var t = modelBuilder.Entity<T>().HasKey(x => x.ID);

            // Using reflection, the relevant properties of type T 
            // are added to the DbSet entity
            foreach (var prop in typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
                if (!prop.CustomAttributes
                    .Any(a => a.AttributeType == typeof(NotMappedAttribute)))
                {
                    t.HasName(prop.Name);
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
