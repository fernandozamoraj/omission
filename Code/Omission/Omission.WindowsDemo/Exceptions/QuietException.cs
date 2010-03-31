using System;

namespace Omission.WindowsDemo.Exceptions
{
    public class QuietException : ApplicationException
    {
        public QuietException(string message): base(message)
        {

        }

        public QuietException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
