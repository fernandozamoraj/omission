using System;
using System.Collections.Generic;

namespace Omission.Framework
{
    public interface IExceptionConfiguration
    {
        //Configuration Methods
        IExceptionConfiguration MapExceptionToHandler<TExceptionType>(IExceptionHandler handler) where TExceptionType : Exception;
        IExceptionConfiguration MapExceptionToLogger<TException>(IExceptionLogger logger);
        IExceptionConfiguration With(IExceptionLogger logger);
        IExceptionConfiguration AndAlso(IExceptionLogger logger);
        IExceptionConfiguration IgnoreExceptionForHandling<TExceptionType>() where TExceptionType : Exception;
        
        //Accessor Methods
        bool ShouldIgnoreForHandling(Type exceptionType);
        List<IExceptionLogger> GetLoggersFor(Exception exception);
        Dictionary<Type, IExceptionHandler> GetHandlers();
        IExceptionHandler GetHandler(Type exceptioNType);
    }
}