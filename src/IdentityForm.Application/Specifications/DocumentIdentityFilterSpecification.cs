using System;
using System.Linq;
using IdentityForm.Domain.Entities;
using IdentityForm.Application.Specifications.Base;

namespace IdentityForm.Application.Specifications
{
    public class DocumentIdentityFilterSpecification : IdentityFormSpecification<DocumentIdentity>
    {
        public DocumentIdentityFilterSpecification(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                Criteria = p =>
                p.FirstName.ToLower().Contains(searchString.ToLower());
            }
            else
            {
                Criteria = p => true;
            }
        }
    }
}
