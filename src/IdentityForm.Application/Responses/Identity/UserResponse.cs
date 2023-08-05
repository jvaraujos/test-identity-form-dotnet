namespace IdentityForm.Application.Responses.Identity
{
    public class UserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
        public int? ZipCode { get; set; }
        public string Number { get; set; }
        public string AddressComplement { get; set; }
        public string Uf { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int? Ibge { get; set; }
        public string XIbge { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; } = true;
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string PictureUrl { get; set; }
        public bool IsOffice { get; set; }
        public bool NotificationWithdrawal { get; set; }

    }
}