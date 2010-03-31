using System;

namespace Omission.WindowsDemo.Exceptions
{
    public class UglyException : ApplicationException
    {
        public UglyException(string message) :
            base(string.Format("An ugly exception has occurred. {0}", message))
        {
        }
    }
}
