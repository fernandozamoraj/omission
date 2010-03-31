using System;
using Omission.Framework;

namespace Omission.UnitTests
{
    public class AnotherFakeLogger : IExceptionLogger
    {
        public void Log(Exception exception)
        {
            throw new NotImplementedException();
        }
    }
}