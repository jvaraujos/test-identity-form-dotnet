using System.ComponentModel.DataAnnotations.Schema;
using System;
using IdentityForm.Domain.Contracts;
using IdentityForm.Domain.Constants;

namespace IdentityForm.Domain.Entities
{
    [Table("DocumentIdentities", Schema = DomainConstants.SchemeName)]
    public class DocumentIdentity : AuditableEntity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Document { get; set; }
        public DateTime BirthDate { get; set; }
        public string FrontFilePath { get; set; }
        public string BackFilePath { get; set; }
        public string CapturedImagePath { get; set; }
    }
}