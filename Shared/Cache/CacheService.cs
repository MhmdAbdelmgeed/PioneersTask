using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Shared.Cache
{
    public class CacheService : ICacheService
    {
        private IDatabase _cacheDb;
        private readonly ILogger<CacheService> _logger;

        public CacheService(ILogger<CacheService> logger)
        {
            _logger = logger;
            try
            {
                var redis = ConnectionMultiplexer.Connect(Services.Redis);
                _cacheDb = redis.GetDatabase();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong in the {nameof(CacheService)}");
            }
        }

        public T GetData<T>(string key)
        {
            RedisValue value = string.Empty;
            if (_cacheDb != null)
            {
                value = _cacheDb.StringGet(key);
            }
            if (!string.IsNullOrEmpty(value))
            {
                //Console.WriteLine($"Data for key {key} was fetched from cache");
                return JsonSerializer.Deserialize<T>(value);
            }
            //Console.WriteLine($"Data for key {key} was fetched from database");

            return default;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (_cacheDb != null)
            {
                var expiryTime = expirationTime - DateTimeOffset.Now;
                return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
            }
            else
                return false;
        }




        public bool RemoveData(string key)
        {
            if (_cacheDb != null)
            {
                var cacheRemoveResult = _cacheDb.KeyDelete(key);
                return cacheRemoveResult;
            }
            else
                return false;
        }


        public bool UpdateData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            if (_cacheDb != null)
            {
                var expiryTime = expirationTime - DateTimeOffset.Now;
                var result = _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
                return result;
            }
            else
                return false;
        }

    }

}
