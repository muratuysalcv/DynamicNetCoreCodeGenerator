using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a data_type.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/data_type/[action]")]
    [Table("data_type", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _data_type : BaseModel
    {
        public string name { get; set; }
    }
}      
