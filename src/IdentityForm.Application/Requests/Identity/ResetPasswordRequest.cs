using System.ComponentModel.DataAnnotations;

namespace IdentityForm.Application.Requests.Identity
{
    public class ResetPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
    }
}