using System;
using System.Collections.Generic;
using System.Data;
using Omission.WindowsDemo.Exceptions;

namespace Omission.WindowsDemo.Model
{
    public class ExceptionLauncher
    {
        List<WrappedException> _exceptions;

        public ExceptionLauncher()
        {
            _exceptions = new List<WrappedException>();

            _exceptions.Add(new WrappedException(new Exception("System.Exception")));
            _exceptions.Add(new WrappedException(new NullReferenceException()));
            _exceptions.Add(new WrappedException(new DataException()));
            _exceptions.Add(new WrappedException(new UglyException("This is one ugly exception! Ooooweee!")));
            _exceptions.Add(new WrappedException(new QuietException("Shh... quiet!")));
            _exceptions.Add(new WrappedException(new ArgumentOutOfRangeException("Count", "Out of range")));
            _exceptions.Add(new WrappedException(new RockyException()));
        }

        public List<WrappedException> GetExceptions()
        {
            return _exceptions;
        }

        public void Throw(Exception exception)
        {
            throw exception;
        }
    }
}