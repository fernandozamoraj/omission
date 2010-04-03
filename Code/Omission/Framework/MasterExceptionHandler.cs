using System;
using System.Collections.Generic;
using Omission.Framework.Logging;
using Omission.Framework.Windows;

namespace Omission.Framework
{
    //The intent of this class is to wrap all exception functionality into
    //one place
    //It will handle the logging to any file system, applicaiton log
    //Also display to the user when necessary
    public class MasterExceptionHandler : IExceptionHandler
    {
        //We need a handler configuration so that
        //we know how to handle specific exceptions
        IExceptionConfiguration _exceptionConfiguration;

        //We need a logging handler so that we can log all
        //exceptions
        //turning the logging handler on or off is up to the logging
        //handler
        IExceptionLogger _defaultLogger;

        //We need a default handler so that all exceptions can be
        //handled some way or another
        IExceptionHandler _defaultHandler;

        public IExceptionLogger DefaultLogger
        {
            get
            {
                return _defaultLogger;
            }
            set
            {
                _defaultLogger = value;
            }
        }

        public IExceptionHandler GetDefaultHandler()
        {
            return _defaultHandler;
        }

        public IExceptionConfiguration GetExceptionConfiguration()
        {
            return _exceptionConfiguration;
        }

        public MasterExceptionHandler()
        {
            EnsureHandlersAreNotNull();
        }

        public MasterExceptionHandler(IExceptionLogger exceptionLogger)
        {
            _defaultLogger = exceptionLogger;
            EnsureHandlersAreNotNull();
        }

        public MasterExceptionHandler(IExceptionLogger exceptionLogger, IExceptionHandler defaultHandler, IExceptionConfiguration handlerConfiguration)
        {
            _exceptionConfiguration = handlerConfiguration;
            _defaultHandler = defaultHandler;
            _defaultLogger = exceptionLogger;

            EnsureHandlersAreNotNull();
        }

        protected void EnsureHandlersAreNotNull()
        {
            IAppConfig appConfig = new DefaultAppConfig();

            if (_defaultLogger == null)
            {
                _defaultLogger = new EventLogLogger(appConfig);
            }

            if (_defaultHandler == null)
            {
                _defaultHandler = new GenericWindowsHandler(appConfig);
            }

            if (_exceptionConfiguration == null)
            {
                _exceptionConfiguration = new DefaultExceptionConfiguration(appConfig);
            }
        }

        public ExceptionHandlingResult Handle(Exception exception)
        {
            Log(exception);

            ExceptionHandlingResult handlingResult = new ExceptionHandlingResult{ReThrow = false, WasHandled = true};

            if(!_exceptionConfiguration.ShouldIgnoreForHandling(exception.GetType()))
            {
                IExceptionHandler handler = _exceptionConfiguration.GetHandler(exception.GetType()) ?? _defaultHandler;

                handlingResult = handler.Handle(exception);
            }

            return handlingResult;
        }

        public void Log(Exception exception)
        {
            bool logged = false;
            List<IExceptionLogger> loggers = _exceptionConfiguration.GetLoggersFor(exception);

            foreach (var logger in loggers)
            {
                logger.Log(exception);
                logged = true;
            }

            if (_defaultLogger != null && !logged)
            {
                _defaultLogger.Log(exception);
            }
        }
    }
}