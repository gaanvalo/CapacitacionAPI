using log4net;
using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace PersonasDTO.LogXNet
{
    public class Logger : ILogAPI 
    {

        
        private static readonly string LOG_CONFIG_FILE = @"log4net.config";

        private static  log4net.ILog _log = null;
        private static  bool _flag= false;

        public Logger(bool flagLog)
        {
            _flag = flagLog;
        }

        public static ILog GetLogger(Type type)
        {
            return LogManager.GetLogger(type);
        }

        public static void Debug(object message)
        {
            SetLog4NetConfiguration();
            _log.Debug(message);
        }
        

        private static void SetLog4NetConfiguration()
        {
            XmlDocument log4netConfig = new XmlDocument();
            log4netConfig.Load(File.OpenRead(LOG_CONFIG_FILE));

            var repo = LogManager.CreateRepository(
            Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));

            log4net.Config.XmlConfigurator.Configure(repo, log4netConfig["log4net"]);
        }

        public void Information(string message, Type typeClass)
        {
            _log = Logger.GetLogger(typeClass);
            if (_flag)
            { SetLog4NetConfiguration(); _log.Info(message); }  
        }

        public void Debug(string message, Type typeClass)
        {
            _log = Logger.GetLogger(typeClass);
            if (_flag)
            { SetLog4NetConfiguration();  _log.Debug(message);}
                
        }

        public void Warning(string message, Type typeClass)
        {
            _log = Logger.GetLogger(typeClass);
            if (_flag)
            { SetLog4NetConfiguration(); _log.Warn(message); }
                
        }

        public void Error(string message, Type typeClass)
        {
            _log = Logger.GetLogger(typeClass);
            SetLog4NetConfiguration();
            _log.Error(message);

        }
    }
}
