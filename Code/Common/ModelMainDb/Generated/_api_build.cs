using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a api_build.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/api_build/[action]")]
    [Table("api_build", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _api_build : BaseModel
    {
        public DateTime date_build { get; set; }

        public Int64 build_number { get; set; }

        public Int64 api_id { get; set; }

        public Int64 builder_user_id { get; set; }

        public string build_dir { get; set; }

        public DateTime? date_build_start { get; set; }

        public DateTime? date_build_end { get; set; }

        public Guid build_uid { get; set; }
    }
}      
