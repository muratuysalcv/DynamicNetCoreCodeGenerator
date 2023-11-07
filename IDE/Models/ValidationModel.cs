using System.ComponentModel.DataAnnotations;

namespace Scriptingo.Admin.Models
{
    public class ValidationModel
    {
        [Required]
        [MinLength(6)]
        public string ValidationCode { get; set; }
    }
}
