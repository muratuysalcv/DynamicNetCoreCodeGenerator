using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a process.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/process/[action]")]
    [Table("process", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _process : BaseModel
    {
        public string name { get; set; }

        public string? description { get; set; }

        public string? sql { get; set; }

        public int con_id { get; set; }

        public bool active { get; set; }

        public string? request { get; set; }

        public string? response { get; set; }
    }
}      
