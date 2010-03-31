namespace Omission.WindowsDemo.Logging
{
    public interface IFile
    {
        bool Exists(string filePath);
        void Create(string filePath);
    }
}