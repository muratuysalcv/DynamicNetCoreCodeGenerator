using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a lang.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/lang/[action]")]
    [Table("lang", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _lang : BaseModel
    {
        public string? name { get; set; }

        public string? culture_code { get; set; }
    }
}      
