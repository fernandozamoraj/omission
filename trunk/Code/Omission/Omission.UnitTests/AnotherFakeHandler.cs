using System;
using Omission.Framework;

namespace Omission.UnitTests
{
    public class AnotherFakeHandler : IExceptionHandler
    {
        public ExceptionHandlingResult Handle(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}