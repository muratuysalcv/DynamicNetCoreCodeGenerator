using System;
using System.ComponentModel.DataAnnotations.Schema;
using Scriptingo.Common;
namespace Scriptingo.Common.Models
{
    public partial class _process
    {
        [NotMapped]
        public virtual string? con_name { get; set; }

        [NotMapped]
        public virtual string? con_type_name { get; set; }
    }
}      
