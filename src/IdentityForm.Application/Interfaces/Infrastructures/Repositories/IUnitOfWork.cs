using IdentityForm.Domain.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityForm.Application.Interfaces.Infrastructures.Repositories
{
    public interface IUnitOfWork<TId> : IDisposable
    {
        IRepositoryAsync<T, TId> Repository<T>() where T : AuditableEntity<TId>;
        IRepositoryNonAuditableAsync<T, TId> RepositoryNonAuditable<T>() where T : NonAuditableEntity<TId>;

        Task<int> Commit(CancellationToken cancellationToken);

        Task<int> CommitAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);

        Task Rollback();
    }
}