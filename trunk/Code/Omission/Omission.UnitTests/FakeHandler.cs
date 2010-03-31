using System;
using Omission.Framework;

namespace Omission.UnitTests
{
    public class FakeHandler : IExceptionHandler
    {
        string _lastExceptionMessage;

        public string LastExceptionMessage
        {
            get
            {
                return _lastExceptionMessage;
            }
        }

        public ExceptionHandlingResult Handle(Exception exception)
        {
            _lastExceptionMessage = exception.Message;

            ExceptionHandlingResult handlingResult = 
                new ExceptionHandlingResult
                    {
                        ReThrow = false, 
                        WasHandled = true
                    };

            return handlingResult;
        }
    }
}