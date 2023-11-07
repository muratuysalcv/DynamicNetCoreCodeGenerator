using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    /// <summary>
    /// Represents a user.
    /// NOTE: This class is generated from a T4 template - you should not modify it manually.
    /// </summary>
    [GeneratedController("api/user/[action]")]
    [Table("user", Schema = "dbo")]
    [FastApiTable("","mssql")]
    public partial class _user : BaseModel
    {
        public string first_name { get; set; }

        public string last_name { get; set; }

        public string e_mail { get; set; }

        public int status { get; set; }

        public string password { get; set; }

        public Guid uid { get; set; }

        public int wrong_try { get; set; }

        public string? temp_code { get; set; }

        public string? jwt_token { get; set; }
    }
}      
