using System;

namespace Omission.Framework
{
    public interface IExceptionLogger
    {
        void Log(Exception exception);
    }
}
