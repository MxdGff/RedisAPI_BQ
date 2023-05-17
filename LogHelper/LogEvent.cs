using log4net;
using log4net.Config;
using log4net.Repository;
using System.Collections;
using System.IO;

namespace LogHelper
{
    public class LogEvent
    {
        #region 属性

        /// <summary>
        /// 默认日志对象
        /// </summary>
        private const string LogName = "Default";

        private static readonly ILoggerRepository LoggerRepository = LogManager.CreateRepository("Xazdyl.LogHelper");
        private static readonly ICollection a = XmlConfigurator.ConfigureAndWatch(LoggerRepository, new FileInfo("log4net.config"));

#pragma warning disable CS0169 // 从不使用字段“LogEvent._Loginfo”
        private static ILog _Loginfo;
#pragma warning restore CS0169 // 从不使用字段“LogEvent._Loginfo”

        /// <summary>
        /// 日志对象
        /// </summary>
        private static ILog LogInfo
        {
            get
            {
                return LogManager.GetLogger(LoggerRepository.Name, LogName);
            }
        }

        /// <summary>
        /// 获取日志对象
        /// </summary>
        /// <returns></returns>
        public static ILog GetLogInfo()
        {
            return LogInfo;
        }

        //如果新加节点使用如下方法
        //public static ILog CUMLogInfo
        //{
        //    get
        //    {
        //        return LogManager.GetLogger(LoggerRepository.Name, "CMU");
        //    }
        //}

        //调用示例
        //LogHelper.LogEvent.Info("zdyltest");
        //LogHelper.LogEvent.Info("zdyl{0}", "test1");

        #endregion

        #region 日志类

        public static void Info(string msg, params object[] args)
        {
            LogInfo.InfoFormat(msg, args);
        }

        public static void Debug(string message, params object[] args)
        {
            LogInfo.DebugFormat(message, args);
        }

        public static void Warn(string msg, params object[] args)
        {
            LogInfo.WarnFormat(msg);
        }

        public static void Error(string msg, params object[] args)
        {
            LogInfo.ErrorFormat(msg);
        }

        public static void Fatal(string msg, params object[] args)
        {
            LogInfo.FatalFormat(msg);
        }

        #endregion
    }
}
