using System;

namespace Omission.WindowsDemo.Model
{
    public class WrappedException
    {
        public Exception Exception{ get; set;}

        public WrappedException(Exception exception)
        {
            Exception = exception;
        }

        public override string  ToString()
        {
            return Exception.GetType().Name;
        }
    }
}