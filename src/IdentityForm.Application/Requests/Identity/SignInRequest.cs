using System.ComponentModel.DataAnnotations;

namespace IdentityForm.Application.Requests.Identity
{
    public class SignInRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}