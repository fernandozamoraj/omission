using Omission.Framework;

namespace Omission.WindowsDemo
{
    public class AppConfig : IAppConfig
    {
        public string GetApplicationName()
        {
            return "Omission Windows Demo";
        }

        public string GetVersion()
        {
            return "1.0";
        }
    }
}