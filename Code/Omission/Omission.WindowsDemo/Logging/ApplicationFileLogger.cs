using System;
using System.Globalization;
using Omission.Framework;
using Omission.WindowsDemo.Environment;

namespace Omission.WindowsDemo.Logging
{
    public class ApplicationFileLogger : IExceptionLogger
    {
        IDateTime _dateTime;
        IAppConfig _appConfig;
        readonly IStreamWriter _streamWriter;
        readonly IFile _File;
        string _prefix;

        public ApplicationFileLogger(string prefix, IDateTime dateTime, IAppConfig appConfig, IFile file, IStreamWriter streamWriter)
        {
            _prefix = prefix;
            _dateTime = dateTime;
            _appConfig = appConfig;
            _streamWriter = streamWriter;
            _File = file;
        }

        public void Log(Exception exception)
        {
            try
            {
                EnsureFileExists();
                OpenFile();
                AppendToFile(GetExceptionMessage(exception));
            }
            finally
            {
                CloseFile();
            }
        }

        void CloseFile()
        {
            _streamWriter.Close();

        }

        string GetExceptionMessage(Exception exception)
        {
            return string.Format("{0} {1} {2} {3}", 
                _dateTime.Now.ToString("yyyy MM dd", CultureInfo.InvariantCulture), 
                _dateTime.Now.ToString("hh:mm:ss.f", CultureInfo.InvariantCulture),
                                exception.GetType(), exception.Message);
        }

        void AppendToFile(string message)
        {
            _streamWriter.WriteLine(message);
        }

        void OpenFile()
        {
            _streamWriter.Open(GetFilePath(), true);
        }

        void EnsureFileExists()
        {
            string fileName = GetFilePath();

            if(!_File.Exists(fileName))
            {
                _streamWriter.Open(fileName, false);
                _streamWriter.Close();
            }
        }

        string GetFilePath()
        {
            DateTime now = _dateTime.Now;

            string fileName = string.Format("{0}_{1}_{2}.log",
                _prefix,
                _appConfig.GetApplicationName(), 
                now.ToString("yyyyMMdd", CultureInfo.InvariantCulture));
            string fullFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
            
            return fullFilePath;
        }
    }
}