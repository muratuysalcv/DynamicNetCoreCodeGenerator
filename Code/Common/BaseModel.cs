using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Scriptingo.Common
{
    public class BaseModel
    {
        [Key]
        public long ID { get; set; }
    }
}
