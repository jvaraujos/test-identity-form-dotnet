namespace IdentityForm.Application.Requests.Identity
{
    public class UpdateProfileRequest
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int? Ibge { get; set; }
        public int? ZipCode { get; set; }
        public string XIbge { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AddressComplement { get; set; }
        public string Uf { get; set; }
        public bool IsOffice { get; set; }

    }
}