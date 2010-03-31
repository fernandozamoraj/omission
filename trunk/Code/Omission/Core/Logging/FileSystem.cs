using System.IO;

namespace Omission.Framework.Logging
{
    public class FileSystem : IFileSystem
    {
        public bool Exists(string filePath)
        {
            return File.Exists(filePath);
        }

        public void Create(string filePath)
        {
            File.Create(filePath);
        }
    }
}