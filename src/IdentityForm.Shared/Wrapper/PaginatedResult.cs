using System;
using System.Collections.Generic;

namespace IdentityForm.Shared.Wrapper
{
    public class PaginatedResult<T> : Result
    {
        public PaginatedResult(List<T> data)
        {
            Data = data;
        }

        public List<T> Data { get; set; }

        internal PaginatedResult(bool succeeded, List<T> data = default, List<string> messages = null, int count = 0, int page = 1, int pageSize = 10)
        {
            Data = data;
            CurrentPage = page;
            Succeeded = succeeded;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
        }

        public static PaginatedResult<T> Failure()
        {
            return new PaginatedResult<T>(false, default, null);
        }

        public static PaginatedResult<T> Failure(string message)
        {
            return new PaginatedResult<T>(false, default, new List<string> { message });
        }

        public static PaginatedResult<T> Failure(List<string> messages)
        {
            return new PaginatedResult<T>(false, default, messages);
        }

        public static new PaginatedResult<T> Success() => new(true, default, null);

        public static new PaginatedResult<T> Success(string message) => new(true, default, new List<string> { message }, 0, 0, 0);

        public static PaginatedResult<T> Success(List<T> data) => new(true, data, null, data.Count, 0, data.Count);

        public static PaginatedResult<T> Success(List<T> data, string message) => new(true, data, new List<string> { message }, data.Count, 0, data.Count);

        public static PaginatedResult<T> Success(List<T> data, List<string> messages) => new(true, data, messages, data.Count, 0, data.Count);

        public static PaginatedResult<T> Success(List<T> data, int count, int page, int pageSize) => new(true, data, null, count, page, pageSize);

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
}