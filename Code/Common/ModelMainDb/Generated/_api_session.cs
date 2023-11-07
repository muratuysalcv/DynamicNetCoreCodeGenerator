using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a api_session.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/api_session/[action]")]
    [Table("api_session", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _api_session : BaseModel
    {
        public Guid session_uid { get; set; }

        public DateTime date_create { get; set; }

        public DateTime? date_end { get; set; }

        public string? jwt { get; set; }
    }
}      
