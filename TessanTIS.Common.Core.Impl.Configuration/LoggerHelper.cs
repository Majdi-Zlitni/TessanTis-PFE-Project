using NLog;
using System;
using System.Collections.Generic;
using System.Text;
using TessanTIS.Common.Core.Abstraction;

namespace TessanTIS.Common.Core.Impl.Configuration
{
    public class LoggerHelper : ILoggerHelper
    {
        private static LoggerHelper instance;
        private static Logger logger;

        private LoggerHelper()
        {

        }
        public static LoggerHelper GetInstance()
        {
            if (instance == null)
                instance = new LoggerHelper();
            return instance;
        }

        private Logger GetCurrentClassLogger()
        {
            if (LoggerHelper.logger == null)
                LoggerHelper.logger = LogManager.GetCurrentClassLogger();
            return LoggerHelper.logger;
        }
        public void Debug(string message, string arg = null)
        {
            if (arg == null)
                GetCurrentClassLogger().Debug(message);
            else
                GetCurrentClassLogger().Debug(message, arg);
        }

        public void Error(string message, string arg = null)
        {
            if(arg == null)
                GetCurrentClassLogger().Error(message);
            else
                GetCurrentClassLogger().Error(message, arg);
        }

        public void Info(string message, string arg = null)
        {
            if (arg == null)
                GetCurrentClassLogger().Info(message);
            else
                GetCurrentClassLogger().Info(message, arg);
        }

        public void Warning(string message, string arg = null)
        {
            if (arg == null)
                GetCurrentClassLogger().Warn(message);
            else
                GetCurrentClassLogger().Warn(message, arg);
        }
    }
}
