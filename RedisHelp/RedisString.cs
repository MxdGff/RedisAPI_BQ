using System;
using System.Collections.Generic;

namespace RedisHelp
{
    public class RedisString : RedisBase
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redisIndex"></param>
        public RedisString(int redisIndex) : base(redisIndex)
        {
        }

        #endregion

        #region 赋值

        /// <summary>
        /// 设置key的value
        /// </summary>
        public bool Set(string key, string value)
        {

            return Core.Set<string>(key, value);
        }

        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public bool Set(string key, string value, DateTime dt)
        {

            return Core.Set<string>(key, value, dt);
        }

        /// <summary>
        /// 设置key的value并设置过期时间
        /// </summary>
        public bool Set(string key, string value, TimeSpan sp)
        {
            return Core.Set<string>(key, value, sp);
        }

        /// <summary>
        /// 设置key的value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="sp"></param>
        /// <returns></returns>
        public bool Set<T>(string key, T value, TimeSpan sp)
        {
            return Core.Set<T>(key, value, sp);
        }

        /// <summary>
        /// 设置多个key/value
        /// </summary>
        public bool Set(Dictionary<string, string> dic)
        {
            Core.SetAll(dic);
            return true;
        }

        #endregion

        #region 获取

        /// <summary>
        /// 获取key的value值
        /// </summary>
        public string Get(string key)
        {
            return Core.GetValue(key);
        }

        /// <summary>
        /// 获取key的value值
        /// </summary>
        public T Get<T>(string key)
        {
            string temp = Core.GetValue(key);
            return ConvertObj<T>(temp);
        }

        #region 删除

        public bool Remove(string key)
        {
            return Core.Remove(key);
        }

        #endregion

        #region 追加

        /// <summary>
        /// 在原有key的value值之后追加value
        /// </summary>
        public long Append(string key, string value, ref bool result)
        {
            result = true;
            return Core.AppendToValue(key, value);
        }

        #endregion

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public List<string> Get(List<string> keys)
        {
            return Core.GetValues(keys);
        }

        /// <summary>
        /// 获取多个key的value值
        /// </summary>
        public List<T> Get<T>(List<string> keys)
        {
            return Core.GetValues<T>(keys);
        }

        #endregion

        #region 获取旧值赋上新值

        /// <summary>
        /// 获取旧值赋上新值
        /// </summary>
        public string GetAndSetValue(string key, string value)
        {
            return Core.GetAndSetValue(key, value);
        }

        #endregion

        #region 辅助方法

        /// <summary>
        /// 获取值的长度
        /// </summary>
        public long GetCount(string key)
        {
            return Core.GetStringCount(key);
        }

        /// <summary>
        /// 自增1，返回自增后的值
        /// </summary>
        public long Incr(string key, ref bool result)
        {
            result = true;
            return Core.IncrementValue(key);
        }

        /// <summary>
        /// 自增count，返回自增后的值
        /// </summary>
        public double IncrBy(string key, double count, ref bool result)
        {
            result = true;
            return Core.IncrementValueBy(key, count);
        }

        /// <summary>
        /// 自减1，返回自减后的值
        /// </summary>
        public long Decr(string key, ref bool result)
        {
            result = true;
            return Core.DecrementValue(key);
        }

        /// <summary>
        /// 自减count ，返回自减后的值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public long DecrBy(string key, int count, ref bool result)
        {
            result = true;
            return Core.DecrementValueBy(key, count);
        }

        #endregion
    }
}
