namespace Shared.Cache
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expirationTime);
        bool UpdateData<T>(string key, T value, DateTimeOffset expirationTime);
        bool RemoveData(string key);
    }
}
