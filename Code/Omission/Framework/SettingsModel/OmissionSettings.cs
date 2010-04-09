using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace Omission.Framework.SettingsModel
{
    /// <summary>
    /// Cant think of what would make a good settings variable
    /// At first I thought you could map certain exceptions to certain
    /// Handlers but then that would mean that you would have to have intimate
    /// knowledge of the Omission Framework
    /// Then I thought well.. what about logging... we could map certain loggers
    /// with certain exceptions but that is more of a logging issues
    /// the default logger would have to handle that
    /// ...
    /// that comes to mind is that of silencing certain exceptions
    /// Other ideas are possibly exposing certain exceptions
    /// Or silencing all exceptions or exposing all exceptions
    /// what about exposing the entire stack for certain exceptions
    /// ...
    /// This is put on hold until I can decide what the proper settings are
    /// ...
    /// </summary>
    public class OmissionSettings
    {
        public static OmissionSettings FromFile(string filePath)
        {
            OmissionSettings omissionSettings;

            using (StreamReader streamReader = new StreamReader(filePath))
            {
                omissionSettings = FromXmlString(streamReader.ReadToEnd());
            }

            return omissionSettings;
        }

        public static OmissionSettings FromXmlString(string xmlString)
        {
            XDocument document = XDocument.Parse(xmlString);

            OmissionSettings omissionSettings = new OmissionSettings();
            
            omissionSettings.LoadFromXml(document.Element("ProductGraph"));

            return omissionSettings;
        }

        public void LoadFromXml(XElement element)
        {
            
        }

        public List<string> ProduceXml()
        {
            return new List<string>();
        }
    }
}