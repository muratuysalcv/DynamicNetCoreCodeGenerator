using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a word.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/word/[action]")]
    [Table("word", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _word : BaseModel
    {
        public string? word { get; set; }

        public string? value { get; set; }

        public string? language { get; set; }
    }
}      
