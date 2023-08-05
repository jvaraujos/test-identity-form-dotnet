using System.ComponentModel.DataAnnotations;

namespace IdentityForm.Application.Requests.Identity
{
    public class ForgotPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}