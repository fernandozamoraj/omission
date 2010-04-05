using System;
using log4net;
using Omission.Framework;

namespace Omission.WindowsDemo.Logging
{
    public class Log4NetLogger : IExceptionLogger
    {
        readonly ILog _log;

        public Log4NetLogger()
        {
            log4net.Config.BasicConfigurator.Configure();
            _log = LogManager.GetLogger("OmissionDemo");
        }

        public void Log(Exception exception)
        {
            _log.Error("Error brought to you by the OmmissionDemo Log4NetLogger " + exception.Message);
        }
    }
}
