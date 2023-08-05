using AutoMapper;
using IdentityForm.Application.Features.DocumentIdentitys.Commands.AddEdit;
using IdentityForm.Domain.Entities;

namespace IdentityForm.Application.Mappings
{
    public class DocumentIdentityProfile : Profile
    {
        public DocumentIdentityProfile()
        {
            CreateMap<AddEditDocumentIdentityCommand, DocumentIdentity>().ReverseMap();
        }
    }
}