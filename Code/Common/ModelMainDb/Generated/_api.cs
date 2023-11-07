using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a api.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/api/[action]")]
    [Table("api", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _api : BaseModel
    {
        public string name { get; set; }

        public string? description { get; set; }

        public bool active { get; set; }

        public string? host { get; set; }

        public Int64 user_id { get; set; }

        public bool is_private { get; set; }

        public Int64? login_process_id { get; set; }

        public Int64? logout_process_id { get; set; }

        public string? jwt_issuer { get; set; }

        public string? jwt_audience { get; set; }

        public string? jwt_key { get; set; }

        public Guid uid { get; set; }

        public Int64 api_code { get; set; }
    }
}      
