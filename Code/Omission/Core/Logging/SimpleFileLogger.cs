using System;
using System.Globalization;
using Omission.Framework.Environment;

namespace Omission.Framework.Logging
{
    public class SimpleFileLogger : IExceptionLogger
    {
        IDateTime _dateTime;
        IAppConfig _appConfig;
        readonly IStreamWriter _streamWriter;
        readonly IFileSystem _File;
        string _filePath;

        public SimpleFileLogger(string filePath)
        {
            _filePath = filePath;
            _dateTime = new OmissionDateTime();
            _appConfig = new DefaultAppConfig();
            _streamWriter = new OStreamWriter();
            _File = new FileSystem();
        }

        public SimpleFileLogger(string filePath, IDateTime dateTime, IAppConfig appConfig, IFileSystem file, IStreamWriter streamWriter)
        {
            _filePath = filePath;
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

            if (!_File.Exists(fileName))
            {
                _streamWriter.Open(fileName, false);
                _streamWriter.Close();
            }
        }

        string GetFilePath()
        {
            return _filePath;
        }
    }
}