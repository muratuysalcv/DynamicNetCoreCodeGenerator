using System.ComponentModel.DataAnnotations;

namespace Scriptingo.Admin.Models
{
    public class ForgotPasswordModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [StringLength(4)]
        [Required]
        public string CaptchaText { get; set; }
        public string? CaptchaBase64 { get; set; }
        public string? CaptchaKey { get; set; }
    }
}
