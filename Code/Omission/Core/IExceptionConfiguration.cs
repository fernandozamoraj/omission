using System;
using System.Collections.Generic;

namespace Omission.Framework
{
    public interface IExceptionConfiguration
    {
        Dictionary<Type, IExceptionHandler> GetHandlers();
        IExceptionConfiguration MapExceptionToHandler<TExceptionType>(IExceptionHandler handler) where TExceptionType : Exception;
        IExceptionConfiguration MapExceptionToLogger<TException>(IExceptionLogger logger);
        IExceptionHandler GetHandler(Type exceptioNType);
        List<IExceptionLogger> GetLoggersFor(Exception exception);
        IExceptionConfiguration With(IExceptionLogger logger);
        IExceptionConfiguration AndAlso(IExceptionLogger logger);
    }
}