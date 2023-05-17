using Microsoft.AspNetCore.Mvc;

namespace RedisAPI_BQ
{
    /// <summary>
    /// 控制器
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RedisAPIController : ControllerBase
    {
        IRedisAPIService _service;
        public RedisAPIController(RedisAPIService service)
        {
            _service = service;
        }

        /// <summary>
        /// 获取RedisString
        /// </summary>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetString(int index, string key)
        {
            try
            {
                return _service.GetRedisString(index, key);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取RedisHash
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public string GetHash(int index, string hashid, string key)
        {
            try
            {
                return _service.GetRedisHash(index, hashid, key);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 获取HashValues
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hashid"></param>
        /// <param name="keys"></param>
        /// <returns></returns>
        [HttpGet]
        public List<string> GetHashValues(int index, string hashid, [FromQuery] List<string> keys)
        {
            try
            {
                List<string> strings = _service.GetRedisHashValues(index, hashid, keys.ToArray());
                return strings;
            }
            catch (Exception ex)
            {
                return new List<string> { ex.Message };
            }
        }

        /// <summary>
        /// 获取RedisList
        /// </summary>
        /// <param name="index"></param>
        /// <param name="hashid"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public List<string> GetList(int index, string hashid, string key)
        {
            try
            {
                return _service.GetRedisList(index, key);
            }
            catch (Exception ex)
            {
                return new List<string> { ex.Message };
            }
        }
    }
}
