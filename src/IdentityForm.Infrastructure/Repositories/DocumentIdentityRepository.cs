using System;
using IdentityForm.Domain.Entities;
using IdentityForm.Application.Interfaces.Infrastructures.Repositories;

namespace IdentityForm.Infrastructure.Repositories
{
    public class DocumentIdentityRepository : IDocumentIdentityRepository
    {
        private readonly IRepositoryAsync<DocumentIdentity, Guid> _repository;

        public DocumentIdentityRepository(IRepositoryAsync<DocumentIdentity, Guid> repository)
        {
            _repository = repository;
        }
    }
}