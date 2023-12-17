namespace Shared
{
    public class Services
    {
        public static readonly string Redis = $"{RedisIpAddress}:{RedisPort}";
        public static readonly string RedisPort = Environment.GetEnvironmentVariable("YIJI_REDIS_PORT");
        public static readonly string RedisIpAddress = Environment.GetEnvironmentVariable("REDIS_IP_ADDRESS");

    }
}
