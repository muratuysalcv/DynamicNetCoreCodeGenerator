using System.ComponentModel.DataAnnotations;
using Scriptingo.Admin.Res;

namespace Scriptingo.Admin.Models
{
    public class RegisterModel
    {
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }
        [MinLength(6)]
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public bool CheckedPolicy { get; set; }
    }
}
