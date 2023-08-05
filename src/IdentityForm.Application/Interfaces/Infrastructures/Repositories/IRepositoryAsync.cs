using IdentityForm.Domain.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityForm.Application.Interfaces.Infrastructures.Repositories
{
    public interface IRepositoryAsync<T, in TId> where T : class, IEntity<TId>
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entity);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entity);

        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entity);
    }
    public interface IRepositoryNonAuditableAsync<T, in TId> where T : class, IEntity<TId>
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(TId id);

        Task<List<T>> GetAllAsync();

        Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entity);

        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(List<T> entity);
    }
}