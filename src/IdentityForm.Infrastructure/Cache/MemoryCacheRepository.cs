using IdentityForm.Application.Interfaces.Infrastructures;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace IdentityForm.Infrastructure.Cache
{
    public class MemoryCacheRepository : IMemoryCacheRepository
    {
        // We will hold 1024 cache entries
        private static readonly int _SIZELIMIT = 1024;
        // A cache entry expire after 15 minutes
        private static readonly int _ABSOLUTEEXPIRATION = 90;

        private MemoryCache Cache { get; set; }

        public MemoryCacheRepository()
        {
            Cache = new MemoryCache(new MemoryCacheOptions
            {
                SizeLimit = _SIZELIMIT
            });
        }

        // Try getting a value from the cache.
        public bool TryGetValue<T>(string key, out T value)
        {
            value = default;

            if (Cache.TryGetValue(key, out T result))
            {
                value = result;
                return true;
            }

            return false;
        }

        // Adding a value to the cache. All entries
        // have size = 1 and will expire after 15 minutes
        public void SetValue<T>(string key, T value)
        {
            Cache.Set(key, value, new MemoryCacheEntryOptions()
              .SetSize(1)
              .SetAbsoluteExpiration(TimeSpan.FromSeconds(_ABSOLUTEEXPIRATION))
            );
        }

        // Remove entry from cache
        public void Remove(string key)
        {
            Cache.Remove(key);
        }
    }
}
