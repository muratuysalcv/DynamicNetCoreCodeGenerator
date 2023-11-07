using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
   
    public partial class _con 
    {
        [NotMapped]
        public virtual string? con_type_name { get; set; }
        public string ConStr()
        {
            var dbConType = new FastApiContext<_con_type>();
            var conType = dbConType.Data.FirstOrDefault(x => x.ID == this.con_type_id);
            string dbType = conType.name;
            string connectionString = conType.con_str_format;
            connectionString = connectionString.Replace("{db_source}", this.db_source);
            connectionString = connectionString.Replace("{db_name}", this.db_name);
            connectionString = connectionString.Replace("{db_password}", this.db_password);
            connectionString = connectionString.Replace("{db_port}", this.db_port + "");
            connectionString = connectionString.Replace("{db_schema}", this.db_schema);
            connectionString = connectionString.Replace("{db_user}", this.db_user);
            return connectionString;
        }
    }
}      
