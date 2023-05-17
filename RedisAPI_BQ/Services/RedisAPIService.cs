using Model;
using RedisHelp;

namespace RedisAPI_BQ
{
    public class RedisAPIService : IRedisAPIService
    {
        public string GetRedisHash(int index, string hashid, string key)
        {
            using RedisHash redisHash = new RedisHash(index);
            string temp = redisHash.GetValueFromHash(hashid, key);
            return temp;
        }

        public List<string> GetRedisHashValues(int index, string hashid, string[] keys)
        {
            using RedisHash redisHash = new RedisHash(index);
            List<string> strings = redisHash.GetValuesFromHash(hashid, keys);
            return strings;
        }

        public string GetRedisString(int index, string key)
        {
            using RedisString redisString = new RedisString(index);
            return redisString.Get(key);
        }

        public T GetRedisString<T>(int index, string key)
        {
            using RedisString redisString = new RedisString(index);
            return redisString.Get<T>(key);
        }

        public List<string> GetRedisList(int index, string key)
        {
            using RedisList redisList = new RedisList(index);
            return redisList.BatchDequeue(key, ConstValue.BatchCount);
        }
    }
}
