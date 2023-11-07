using System.ComponentModel.DataAnnotations;
namespace Scriptingo.Admin.Models
{
    public class PasswordResetModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(32)]
        [MaxLength(32)]
        [MinLength(32)]
        public string ResetCode { get; set; }

        [Required]
        [StringLength(6)]
        [MaxLength(6)]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
