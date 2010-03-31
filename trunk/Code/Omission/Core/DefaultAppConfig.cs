using System;
using System.Reflection;

namespace Omission.Framework
{
    public class DefaultAppConfig : IAppConfig
    {
        public string GetApplicationName()
        {
            return Assembly.GetExecutingAssembly().FullName;
        }

        public string GetVersion()
        {
            return "1.0";
        }
    }
}