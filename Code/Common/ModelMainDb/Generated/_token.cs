using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a token.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/token/[action]")]
    [Table("token", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _token : BaseModel
    {
        public DateTime date_create { get; set; }

        public DateTime? date_end { get; set; }

        public string jwt { get; set; }

        public Int64 user_id { get; set; }
    }
}      
