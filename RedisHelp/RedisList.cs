using ServiceStack.Redis;
using ServiceStack.Redis.Pipeline;
using System;
using System.Collections.Generic;


namespace RedisHelp
{
    public class RedisList : RedisBase
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="redisIndex"></param>
        public RedisList(int redisIndex) : base(redisIndex)
        {
        }

        #endregion

        #region 赋值

        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public bool LPush(string key, string value)
        {
            Core.PushItemToList(key, value);
            return true;
        }

        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public bool LPush<T>(string key, T value)
        {
            string temp = ConvertJson(value);
            Core.PushItemToList(key, temp);
            return true;
        }
        /// <summary>
        /// 从左侧向list中添加值，并设置过期时间
        /// </summary>
        public bool LPush(string key, string value, DateTime dt)
        {
            Core.PushItemToList(key, value);
            Core.ExpireEntryAt(key, dt);
            return true;
        }

        /// <summary>
        /// 从左侧向list中添加值，设置过期时间
        /// </summary>
        public bool LPush(string key, string value, TimeSpan sp)
        {

            Core.PushItemToList(key, value);
            Core.ExpireEntryIn(key, sp);
            return true;
        }

        /// <summary>
        /// 从左侧向list中添加值
        /// </summary>
        public bool RPush(string key, string value)
        {
            Core.PrependItemToList(key, value);
            return true;
        }
        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>    
        public bool RPush(string key, string value, DateTime dt)
        {
            Core.PrependItemToList(key, value);
            Core.ExpireEntryAt(key, dt);
            return true;
        }

        /// <summary>
        /// 从右侧向list中添加值，并设置过期时间
        /// </summary>        
        public bool RPush(string key, string value, TimeSpan sp)
        {
            Core.PrependItemToList(key, value);
            Core.ExpireEntryIn(key, sp);
            return true;
        }
        /// <summary>
        /// 添加key/value
        /// </summary>     
        public bool Add(string key, string value)
        {
            Core.AddItemToList(key, value);
            return true;
        }
        /// <summary>
        /// 添加key/value ,并设置过期时间
        /// </summary>  
        public bool Add(string key, string value, DateTime dt)
        {
            Core.AddItemToList(key, value);
            Core.ExpireEntryAt(key, dt);
            return true;
        }

        /// <summary>
        /// 添加key/value。并添加过期时间
        /// </summary>  
        public bool Add(string key, string value, TimeSpan sp)
        {
            Core.AddItemToList(key, value);
            Core.ExpireEntryIn(key, sp);
            return true;
        }

        /// <summary>
        /// 为key添加多个值
        /// </summary>  
        public bool Add(string key, List<string> values)
        {
            Core.AddRangeToList(key, values);
            return true;
        }

        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public bool Add(string key, List<string> values, DateTime dt)
        {
            Core.AddRangeToList(key, values);
            Core.ExpireEntryAt(key, dt);
            return true;
        }
        /// <summary>
        /// 为key添加多个值，并设置过期时间
        /// </summary>  
        public bool Add(string key, List<string> values, TimeSpan sp)
        {
            Core.AddRangeToList(key, values);
            Core.ExpireEntryIn(key, sp);
            return true;
        }
        #endregion

        #region 获取值

        /// <summary>
        /// 获取list中key包含的数据数量
        /// </summary>  
        public long Count(string key)
        {
            return Core.GetListCount(key);
        }
        /// <summary>
        /// 获取key包含的所有数据集合
        /// </summary>  
        public List<string> Get(string key)
        {
            return Core.GetAllItemsFromList(key);
        }
        /// <summary>
        /// 获取key中下标为star到end的值集合
        /// </summary>  
        public List<string> Get(string key, int star, int end)
        {
            return Core.GetRangeFromList(key, star, end);
        }
        
        #endregion

        #region 阻塞命令
        
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingPopItemFromList(string key, TimeSpan? sp)
        {
            return Core.BlockingDequeueItemFromList(key, sp);
        }
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public ItemRef BlockingPopItemFromLists(string[] keys, TimeSpan? sp)
        {
            return Core.BlockingPopItemFromLists(keys, sp);
        }
        /// <summary>
        ///  阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingDequeueItemFromList(string key, TimeSpan? sp)
        {
            return Core.BlockingDequeueItemFromList(key, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中keys的尾部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public ItemRef BlockingDequeueItemFromLists(string[] keys, TimeSpan? sp)
        {
            return Core.BlockingDequeueItemFromLists(keys, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中key的头部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingRemoveStartFromList(string keys, TimeSpan? sp)
        {
            return Core.BlockingRemoveStartFromList(keys, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中key的头部移除一个值，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public ItemRef BlockingRemoveStartFromLists(string[] keys, TimeSpan? sp)
        {
            return Core.BlockingRemoveStartFromLists(keys, sp);
        }
        /// <summary>
        /// 阻塞命令：从list中一个fromkey的尾部移除一个值，添加到另外一个tokey的头部，并返回移除的值，阻塞时间为sp
        /// </summary>  
        public string BlockingPopAndPushItemBetweenLists(string fromkey, string tokey, TimeSpan? sp)
        {
            return Core.BlockingPopAndPushItemBetweenLists(fromkey, tokey, sp);
        }
        
        #endregion

        #region 删除

        /// <summary>
        /// 从尾部移除数据，返回移除的数据
        /// </summary>  
        public string PopItemFromList(string key)
        {
            return Core.PopItemFromList(key);
        }

        /// <summary>
        /// 删除所有队列
        /// </summary>
        /// <param name="listId"></param>
        public void RemoveAllFromList(string listId)
        {
            Core.RemoveAllFromList(listId);
        }

        /// <summary>
        /// 移除list中，key/value,与参数相同的值，并返回移除的数量
        /// </summary>  
        public long RemoveItemFromList(string key, string value)
        {
            return Core.RemoveItemFromList(key, value);
        }
        /// <summary>
        /// 从list的尾部移除一个数据，返回移除的数据
        /// </summary>  
        public string RemoveEndFromList(string key)
        {
            return Core.RemoveEndFromList(key);
        }
        /// <summary>
        /// 从list的头部移除一个数据，返回移除的值
        /// </summary>  
        public string RemoveStartFromList(string key)
        {
            return Core.RemoveStartFromList(key);
        }
        #endregion

        #region 其它

        /// <summary>
        /// 从一个list的尾部移除一个数据，添加到另外一个list的头部，并返回移动的值
        /// </summary>  
        public string PopAndPushItemBetweenLists(string fromKey, string toKey)
        {
            return Core.PopAndPushItemBetweenLists(fromKey, toKey);
        }
        
        #endregion

        #region 批量

        /// <summary>
        /// 从头批量获取消息队列
        /// </summary>
        /// <param name="listID"></param>
        /// <param name="max_count"></param>
        /// <returns></returns>
        public List<string> BatchDequeue(string listID, int max_count)
        {
            List<string> ret = new List<string>();

            using (IRedisPipeline pipeline = ((RedisClient)Core).CreatePipeline())              //Calls 'MULTI'
            {
                pipeline.QueueCommand(
                    x => x.GetRangeFromList(listID, 0, max_count - 1),
                    y =>
                    {
                        if (y != null && y.Count > 0)
                            ret.AddRange(y);
                    }
                    );
                pipeline.QueueCommand(x => x.TrimList(listID, max_count, -1));

                pipeline.Flush();                                        //Calls 'EXEC'

            }                                                          //Calls 'DISCARD' if 'EXEC' wasn't called

            return ret;
        }

        /// <summary>
        /// 批量删除key数据
        /// </summary>
        /// <param name="hashid"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        public bool RightPushList(string hashid, List<string> values)
        {

            using (IRedisPipeline redis = Core.CreatePipeline())
            {
                foreach (string value in values)
                {
                    redis.QueueCommand(r => r.PrependItemToList(hashid, value));
                }
                redis.Flush();
            }

            return true;
        }

        #endregion

    }
}
