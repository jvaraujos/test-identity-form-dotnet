using IdentityForm.Application.Exceptions;
using IdentityForm.Application.Specifications.Base;
using IdentityForm.Domain.Contracts;
using IdentityForm.Shared.Wrapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityForm.Application.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new ApiException();
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = await source.CountAsync();
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable().ToList();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
        public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this List<T> source, int pageNumber, int pageSize) where T : class
        {
            if (source == null) throw new ApiException();
            pageNumber = pageNumber == 0 ? 1 : pageNumber;
            pageSize = pageSize == 0 ? 10 : pageSize;
            int count = source.Count;
            pageNumber = pageNumber <= 0 ? 1 : pageNumber;
            List<T> items = source.AsQueryable().Skip((pageNumber - 1) * pageSize).Take(pageSize).AsEnumerable().ToList();
            return PaginatedResult<T>.Success(items, count, pageNumber, pageSize);
        }
        public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class, IEntity
        {
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(query,
                    (current, include) => current.Include(include));
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));
            return secondaryResult.Where(spec.Criteria);
        }
    }
}