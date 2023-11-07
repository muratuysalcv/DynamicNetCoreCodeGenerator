using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a api_token.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/api_token/[action]")]
    [Table("api_token", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _api_token : BaseModel
    {
        public string name { get; set; }

        public Guid uid { get; set; }

        public bool active { get; set; }
    }
}      
