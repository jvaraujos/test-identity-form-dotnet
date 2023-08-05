using IdentityForm.Infrastructure.Shared.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Services.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public MemoryCacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddHours(1),
                Priority = CacheItemPriority.High,
                SlidingExpiration = TimeSpan.FromMinutes(30)
            };
        }
        public bool TryGet<T>(string cacheKey, out T value)
        {
            _memoryCache.TryGetValue(cacheKey, out value);
            if (value == null) return false;
            else return true;
        }
        public T SetString<T>(string cacheKey, T value, int slindingExpirationInMinutes = 10, int absoluteExpirationRelativeToNowInHours = 1)
        {
            return _memoryCache.Set(cacheKey, value, _cacheOptions);
        }
        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }


        public string Get(string key)
        {
            throw new NotImplementedException();
        }

        public T StringSet<T>(string cacheKey, T value, int slindingExpirationInMinutes = 10, int absoluteExpirationRelativeToNowInHours = 1)
        {
            throw new NotImplementedException();
        }

        public string StringGet(string key)
        {
            throw new NotImplementedException();
        }

        public T Set<T>(string cacheKey, T value, int slindingExpirationInMinutes = 10, int absoluteExpirationRelativeToNowInHours = 1)
        {
            throw new NotImplementedException();
        }

        public Task<List<RedisKey>> GetKeysFromPattern(string patternName)
        {
            throw new NotImplementedException();
        }
    }
}
