using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityForm.Application.Requests.Identity
{
    public class RegisterRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int? ZipCode { get; set; }
        public string Uf { get; set; }
        public string Document { get; set; }
        public int? Ibge { get; set; }
        public string XIbge { get; set; }
        public string Address { get; set; }
        public string Number { get; set; }
        public string AddressComplement { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsOffice { get; set; }
        public Guid? SubscribeId { get; set; }
        public Guid? ProfileId { get; set; }
    }
}