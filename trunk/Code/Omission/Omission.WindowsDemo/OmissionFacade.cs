using System;
using System.Collections.Generic;
using Omission.Framework;
using Omission.Framework.Environment;
using Omission.WindowsDemo.Exceptions;
using Omission.Framework.Logging;

namespace Omission.WindowsDemo
{
    public static class OmissionFacade
    {
        static MasterExceptionHandler _masterExceptionHandler;
        
        public static ExceptionHandlingResult Handle(Exception exception)
        {
            EnsureExceptionHandlerExists();

            return _masterExceptionHandler.Handle(exception);
        }

        static void EnsureExceptionHandlerExists()
        {
            if (_masterExceptionHandler == null)
            {
                DefaultExceptionConfiguration defaultExceptionConfiguration = 
                    new DefaultExceptionConfiguration(new AppConfig());

                _masterExceptionHandler = new MasterExceptionHandler(
                    new SimpleFileLogger(new FileNameCreator().GetFilePath("Main"), new OmissionDateTime(), new AppConfig(), new FileSystem(),
                                              new OStreamWriter()),
                    new OmissionWindowsHandler(new AppConfig()),
                    defaultExceptionConfiguration);


                Configure(defaultExceptionConfiguration);
                
            }
        }

        static void Configure(IExceptionConfiguration configuration)
        {
            List<IExceptionLogger> loggers = new List<IExceptionLogger>();

            loggers.Add(new SimpleFileLogger(new FileNameCreator().GetFilePath("SilentLogger"),
                new OmissionDateTime(), 
                new AppConfig(), 
                new FileSystem(),
                new OStreamWriter()));

            configuration
                .MapExceptionToHandler<QuietException>(new LogOnlyExceptionHandler(loggers))
                .MapExceptionToHandler<UglyException>(new MyUglyErrorMessageHandler());
        }
    }
}
