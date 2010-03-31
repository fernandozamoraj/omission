using System.IO;

namespace Omission.WindowsDemo.Logging
{
    public class OmissionFile : IFile
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