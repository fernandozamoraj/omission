namespace Omission.Framework.Logging
{
    public interface IFileSystem
    {
        bool Exists(string filePath);
        void Create(string filePath);
    }
}