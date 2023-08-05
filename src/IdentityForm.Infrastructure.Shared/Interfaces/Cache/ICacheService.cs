using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityForm.Infrastructure.Shared.Interfaces.Cache
{
    public interface ICacheService
    {
        string StringGet(string key);
        T Get<T>(string key);
        Task<List<RedisKey>> GetKeysFromPattern(string patternName);
        T Set<T>(string cacheKey, T value, int slindingExpirationInMinutes = 10, int absoluteExpirationRelativeToNowInHours = 1);
        void Remove(string cacheKey);
    }
}
