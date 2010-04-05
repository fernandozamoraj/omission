using System;

namespace Omission.WindowsDemo.Exceptions
{
    public class RockyException : Exception
    {
        public RockyException() : base("This will never happen - Rocky")
        {

        }
    }
}
