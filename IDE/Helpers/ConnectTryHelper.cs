using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;
using Scriptingo.Common;
using Scriptingo.Common.Models;

namespace Scriptingo.Admin.Helpers
{
    public class ConnectTryHelper
    {
        public static bool Connect(_con con)
        {
            bool result = false;
            var conType = (new FastApiContext<_con_type>()).Data.FirstOrDefault(x => x.ID == con.con_type_id);
            try
            {
                if (conType.name == Config.MsSql)
                {
                    var c = new SqlConnection(con.ConStr());
                    c.Open();
                    c.Close();
                }
                else if (conType.name == Config.MySql)
                {
                    var c = new MySqlConnection(con.ConStr());
                    c.Open();
                    c.Close();
                }
                else if (conType.name == Config.Oracle)
                {
                    var c = new OracleConnection(con.ConStr());
                    c.Open();
                    c.Close();
                }
                else if (conType.name == Config.PostgreSql)
                {
                    var c = new Npgsql.NpgsqlConnection(con.ConStr());
                    c.Open();
                    c.Close();
                }
                else if (conType.name == Config.Sqlite)
                {
                    var c = new SqliteConnection(con.ConStr());
                    c.Open();
                    c.Close();
                }
                else
                {
                    throw new Exception("Connection type is not know.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
    }
}
