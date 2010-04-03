using System;

namespace Omission.Framework
{
    public interface IExceptionHandler
    {
        ExceptionHandlingResult Handle(Exception exception);
    }
}