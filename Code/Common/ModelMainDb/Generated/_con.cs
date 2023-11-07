using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a con.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/con/[action]")]
    [Table("con", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _con : BaseModel
    {
        public string name { get; set; }

        public string? connection { get; set; }

        public int con_type_id { get; set; }

        public bool active { get; set; }

        public Int64? user_id { get; set; }

        public string? db_source { get; set; }

        public string? db_user { get; set; }

        public string? db_password { get; set; }

        public string? db_name { get; set; }

        public int? db_port { get; set; }

        public string? db_schema { get; set; }

        public string? comment { get; set; }

        public Int64 api_id { get; set; }

        public bool is_main { get; set; }

        public bool is_scaffold { get; set; }
    }
}      
