using System.ComponentModel.DataAnnotations;

namespace IdentityForm.Application.Requests.Identity
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string NewPassword { get; set; }

        [Required]
        public string ConfirmNewPassword { get; set; }
    }
}