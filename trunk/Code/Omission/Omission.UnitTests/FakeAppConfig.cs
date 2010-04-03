using Omission.Framework;

namespace Omission.UnitTests
{
    public class FakeAppConfig : IAppConfig
    {
        string _version;
        string _applicationName;

        public FakeAppConfig()
        {
            Initialize("UnitTests", "0.1");
        }

        public FakeAppConfig(string applicationName, string version)
        {
           Initialize(applicationName, version);

        }

        public string GetApplicationName()
        {
            return _applicationName;
        }

        public string GetVersion()
        {
            return _version;
        }

        protected void Initialize(string applicationName, string version)
        {
            _applicationName = applicationName;
            _version = version;
        }
    }
}