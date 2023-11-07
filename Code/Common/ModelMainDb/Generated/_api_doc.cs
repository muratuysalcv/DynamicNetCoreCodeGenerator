using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a api_doc.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/api_doc/[action]")]
    [Table("api_doc", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _api_doc : BaseModel
    {
        public string name { get; set; }

        public string? description { get; set; }
    }
}      
