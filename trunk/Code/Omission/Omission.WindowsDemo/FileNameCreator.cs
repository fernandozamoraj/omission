using System;
using System.Globalization;
using System.IO;

namespace Omission.WindowsDemo
{
    public class FileNameCreator
    {
        public string GetFilePath(string prefix)
        {
            DateTime now = DateTime.Now;

            string fileName = string.Format("{0}_{1}.log",
                                            prefix,
                                            now.ToString("yyyyMMdd", CultureInfo.InvariantCulture));

            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        }
    }
}