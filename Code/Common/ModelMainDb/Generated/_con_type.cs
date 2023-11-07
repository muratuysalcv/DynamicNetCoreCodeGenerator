using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a con_type.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/con_type/[action]")]
    [Table("con_type", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _con_type : BaseModel
    {
        public string name { get; set; }

        public string? description { get; set; }

        public string? con_str_format { get; set; }
    }
}      
