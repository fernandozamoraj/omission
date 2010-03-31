namespace Omission.WindowsDemo.Logging
{
    public interface IStreamWriter
    {
        void Open(string filePath, bool append);
        void Close();
        void WriteLine(string line);
    }
}