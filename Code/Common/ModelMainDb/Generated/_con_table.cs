using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a con_table.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/con_table/[action]")]
    [Table("con_table", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _con_table : BaseModel
    {
        public string name { get; set; }

        public string? description { get; set; }

        public bool active { get; set; }

        public Int64 con_id { get; set; }
    }
}      
