using System;
using System.Collections.Generic;
using Omission.Framework;

namespace Omission.WindowsDemo.Exceptions
{
    public class LogOnlyExceptionHandler : IExceptionHandler
    {
        List<IExceptionLogger> _loggers;

        public LogOnlyExceptionHandler(List<IExceptionLogger> loggers)
        {
            _loggers = loggers;
        }

        public ExceptionHandlingResult Handle(Exception exception)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(exception);
            }

            return new ExceptionHandlingResult
                       {
                           ReThrow = false, 
                           WasHandled = true
                       };
        }
    }
}
