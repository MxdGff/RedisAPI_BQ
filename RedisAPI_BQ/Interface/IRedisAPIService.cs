namespace RedisAPI_BQ
{
    public interface IRedisAPIService
    {
        string GetRedisHash(int index, string hashid, string key);

        List<string> GetRedisHashValues(int index, string hashid, string[] keys);

        string GetRedisString(int index, string key);

        T GetRedisString<T>(int index, string key);

        List<string> GetRedisList(int index, string key);
    }
}
