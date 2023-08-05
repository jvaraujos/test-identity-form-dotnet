using System;

namespace IdentityForm.Application.Responses.Payments
{
    public class PaymentResponse
    {
        public Guid OrderId { get; set; }
        public string PaymentLink { get; set; }
        public string PreferenceId { get; set; }
    }
}
