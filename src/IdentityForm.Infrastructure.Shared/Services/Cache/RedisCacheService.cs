using IdentityForm.Infrastructure.Shared.Interfaces.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Services.Cache
{
    public class RedisCacheService : ICacheService
    {
        private readonly ILogger<RedisCacheService> _logger;
        private readonly IDistributedCache _cache;
        private readonly ConnectionMultiplexer _connection;
        private readonly EndPoint _endPoint;
        private readonly IDatabase _database;

        public RedisCacheService(ILogger<RedisCacheService> logger, IDistributedCache cache, IConfiguration configuration)
        {
            _logger = logger;
            _cache = cache;
            ConfigurationOptions options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis"));
            options.DefaultDatabase = 1;
            _connection = ConnectionMultiplexer.Connect(options);
            _database = _connection.GetDatabase();
            _endPoint = _connection.GetEndPoints().First();
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public T Set<T>(string cacheKey, T value, int slindingExpirationInMinutes = 10, int absoluteExpirationRelativeToNowInHours = 1)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(absoluteExpirationRelativeToNowInHours),
                SlidingExpiration = TimeSpan.FromMinutes(slindingExpirationInMinutes)
            };
            _cache.SetString(cacheKey, JsonConvert.SerializeObject(value), options);
            return value;
        }

        public T Get<T>(string key)
        {
            var value = _cache.GetString(key);
            if (value != null)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            return default;
        }

        public string StringGet(string cacheKey)
        {
            var value = _database.StringGet(cacheKey);
            return value;
        }

        public Task<List<RedisKey>> GetKeysFromPattern(string patternName)
        {
            var keys = _connection.GetServer(_endPoint).Keys(pattern: $"{patternName}:*").ToList();
            return Task.FromResult(keys);
        }
    }
}
