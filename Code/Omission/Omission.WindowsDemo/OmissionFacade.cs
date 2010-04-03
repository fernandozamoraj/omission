using System;
using Omission.Framework;
using Omission.Framework.Environment;
using Omission.Framework.Logging;

namespace Omission.WindowsDemo
{
    public static class OmissionFacade
    {
        static MasterExceptionHandler _masterExceptionHandler;

        public static MasterExceptionHandler GetExceptionHandler()
        {
            EnsureExceptionHandlerExists();

            return _masterExceptionHandler;
        }

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
            }
        }
    }
}
