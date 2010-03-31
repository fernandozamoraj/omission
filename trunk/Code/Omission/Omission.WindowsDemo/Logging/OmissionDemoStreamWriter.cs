using System.IO;

namespace Omission.WindowsDemo.Logging
{
    public class OmissionDemoStreamWriter : IStreamWriter
    {
        StreamWriter _streamWriter;
        public void Open(string filePath, bool append)
        {
            _streamWriter = new StreamWriter(filePath, append);
        }

        public void Close()
        {
            _streamWriter.Close();
            _streamWriter.Dispose();
            _streamWriter = null;
        }

        public void WriteLine(string line)
        {
            _streamWriter.WriteLine(line);
        }
    }
}