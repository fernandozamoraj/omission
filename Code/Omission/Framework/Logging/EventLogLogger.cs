using System;
using System.Diagnostics;

namespace Omission.Framework.Logging
{
    public class EventLogLogger : IExceptionLogger
    {
        IAppConfig _appConfig;

        public EventLogLogger()
        {

        }

        public EventLogLogger(IAppConfig appConfig)
        {
            _appConfig = appConfig;
        }

        public void Log(Exception exception)
        {
            Log(exception);
        }

        protected void LogDetails(Exception exception)
        {
            EnsureItHasAppConfig();

            //If the uppermost exception is the same as the base then only show the base, else show both
            string logEntry;

            if (exception.Message != exception.GetBaseException().Message)
            {
                logEntry = "Uppermost exception ... \r\nVersion: " + _appConfig.GetVersion()
                           + "\r\nMessage: " + exception.Message
                           + "\r\nSource: " + exception.Source
                           + "\r\nStack Trace: " + exception.StackTrace;
                WriteEntryToAppLog(logEntry, EventLogEntryType.Error);
            }

            logEntry = "Base exception... \r\nVersion: " + _appConfig.GetVersion()
                       + "\r\nMessage: " + exception.GetBaseException().Message
                       + "\r\nSource: " + exception.GetBaseException().Source
                       + "\r\nStack Trace: " + exception.GetBaseException().StackTrace;
            WriteEntryToAppLog(logEntry, EventLogEntryType.Error);
        }

        void EnsureItHasAppConfig()
        {
            if (_appConfig == null)
            {
                _appConfig = new DefaultAppConfig();
            }
        }

        protected void WriteEntryToAppLog(string message)
        {
            WriteEntryToAppLog(message, EventLogEntryType.Information);
        }

        protected void WriteEntryToAppLog(string message, EventLogEntryType entryType)
        {
            string appName = _appConfig.GetApplicationName();

            //only write to log if source exists since web services cannot create a new source
            //Need to investigate if exceptions are being thrown to the log at all in sams1e
            //It appears as if they are not
            if (EventLog.SourceExists(appName))
            {
                EventLog.WriteEntry(appName, message, entryType);
            }
            else// (EventLog.SourceExists(appName))
            {
                EventLog.CreateEventSource(appName, appName);
                EventLog.WriteEntry(appName, message, entryType);
            }

        }
    }
}