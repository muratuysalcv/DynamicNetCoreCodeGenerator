using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a process_param.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/process_param/[action]")]
    [Table("process_param", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _process_param : BaseModel
    {
        public string name { get; set; }

        public string data_type { get; set; }

        public int direction { get; set; }

        public int priority { get; set; }

        public string? description { get; set; }
    }
}      
