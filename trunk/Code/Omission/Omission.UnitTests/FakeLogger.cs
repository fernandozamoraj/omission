using System;
using System.Collections.Generic;
using Omission.Framework;

namespace Omission.UnitTests
{
    public class FakeLogger : IExceptionLogger
    {
        List<string> _logMessages;

        public FakeLogger()
        {
            _logMessages = new List<string>();
        }

        public void Log(Exception exception)
        {
            _logMessages.Add(exception.Message);
        }
    }
}