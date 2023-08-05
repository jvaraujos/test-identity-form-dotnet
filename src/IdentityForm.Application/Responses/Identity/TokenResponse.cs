using System;

namespace IdentityForm.Application.Responses.Identity
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public Guid SubscribeId { get; set; }
        public Guid UserId { get; set; }
    }
}