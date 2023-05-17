using ServiceStack.Redis.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisHelp
{
    public class RedisHash : RedisBase
    {
        #region 属性
        private static readonly object lockHelper = new object();
        public const int MaxMultiCount = 100000;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redisIndex"></param>
        public RedisHash(int redisIndex) : base(redisIndex)
        {
        }

        #endregion

        #region 添加

        /// <summary>
        /// 将List设置到Hash中去
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="keyValuePairs"></param>
        /// <returns></returns>
        public bool SetListInHash(string hashid, IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            while (keyValuePairs.Count() > MaxMultiCount)
            {
                List<KeyValuePair<string, string>> temps = keyValuePairs.Take(MaxMultiCount).ToList();
                Core.SetRangeInHash(hashid, temps);

                keyValuePairs = keyValuePairs.Skip(MaxMultiCount).ToList();
            }


            Core.SetRangeInHash(hashid, keyValuePairs);
            return true;
        }

        /// <summary>
        /// 向hashid集合中添加key/value
        /// </summary>       
        public bool SetEntryInHash(string hashid, string key, string value)
        {
            return Core.SetEntryInHash(hashid, key, value);

        }
        public bool ExpireEntryIn(string key, TimeSpan expireIn)
        {
            return Core.ExpireEntryIn(key, expireIn);
        }
        /// <summary>
        /// 向hashid集合中添加key/value
        /// </summary>       
        public bool SetEntryInHash<T>(string hashid, string key, T value)
        {
            string json = ConvertJson(value);
            return Core.SetEntryInHash(hashid, key, json);
        }

        /// <summary>
        /// 如果hashid集合中存在key/value则不添加返回false，如果不存在在添加key/value,返回true
        /// </summary>
        public bool SetEntryInHashIfNotExists(string hashid, string key, string value)
        {
            return Core.SetEntryInHashIfNotExists(hashid, key, value);
        }
        /// <summary>
        /// 存储对象T t到hash集合中
        /// </summary>
        public void StoreAsHash<T>(T t)
        {
            Core.StoreAsHash<T>(t);
        }

        #endregion

        #region 获取

        /// <summary>
        /// 获取对象T中ID为id的数据。
        /// </summary>
        public T GetFromHash<T>(object id)
        {
            return Core.GetFromHash<T>(id);
        }
        /// <summary>
        /// 获取所有hashid数据集的key/value数据集合
        /// </summary>
        public Dictionary<string, string> GetAllEntriesFromHash(string hashid)
        {
            return Core.GetAllEntriesFromHash(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中的数据总数
        /// </summary>
        public long GetHashCount(string hashid)
        {
            return Core.GetHashCount(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中所有key的集合
        /// </summary>
        public List<string> GetHashKeys(string hashid)
        {
            return Core.GetHashKeys(hashid);
        }
        /// <summary>
        /// 获取hashid数据集中的所有value集合
        /// </summary>
        public List<string> GetHashValues(string hashid)
        {
            return Core.GetHashValues(hashid);
        }

        /// <summary>
        /// 获取hashid数据集中，key的value数据
        /// </summary>
        public string GetValueFromHash(string hashid, string key)
        {
            return Core.GetValueFromHash(hashid, key);
        }

        /// <summary>
        /// 获取hashid数据集中，key的value数据
        /// </summary>
        public T GetValueFromHash<T>(string hashid, string key)
        {
            string obj = Core.GetValueFromHash(hashid, key);
            return ConvertObj<T>(obj);
        }

        /// <summary>
        /// 获取hashid数据集中，多个keys的value集合
        /// </summary>
        public List<string> GetValuesFromHash(string hashid, string[] keys)
        {
            List<string> tempList = Core.GetValuesFromHash(hashid, keys);
            return tempList.Where(p => p != null).ToList();
        }

        /// <summary>
        /// 获取hashid数据集中，多个keys的value集合
        /// </summary>
        public List<T> GetValuesFromHash<T>(string hashid, string[] keys)
        {
            List<T> ts = new List<T>();
            List<string> tempList = Core.GetValuesFromHash(hashid, keys);

            foreach (string temp in tempList)
            {
                T t = ConvertObj<T>(temp);
                if (t != null)
                {
                    ts.Add(t);
                }
            }

            return ts;
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除hashid数据集中的key数据
        /// </summary>
        public bool RemoveEntryFromHash(string hashid, string key)
        {
            return Core.RemoveEntryFromHash(hashid, key);
        }

        /// <summary>
        /// 批量删除key数据
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool RemoveListFromHash(string hashid, List<string> keys)
        {
            using (IRedisPipeline redis = Core.CreatePipeline())
            {
                foreach (string key in keys)
                {
                    redis.QueueCommand(r => r.RemoveEntryFromHash(hashid, key));
                }
                redis.Flush();
            }

            return true;
        }

        #endregion

        #region 其它

        /// <summary>
        /// 判断hashid数据集中是否存在key的数据
        /// </summary>
        public bool HashContainsEntry(string hashid, string key)
        {
            return Core.HashContainsEntry(hashid, key);
        }
        /// <summary>
        /// 给hashid数据集key的value加countby，返回相加后的数据
        /// </summary>
        public double IncrementValueInHash(string hashid, string key, double countBy)
        {
            return Core.IncrementValueInHash(hashid, key, countBy);
        }

        #endregion
    }
}
