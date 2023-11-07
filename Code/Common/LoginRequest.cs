using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scriptingo.Common
{
    public class LoginRequest
    {
        public string Api { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class RegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string RegisterToken { get; set; }

    }
}
