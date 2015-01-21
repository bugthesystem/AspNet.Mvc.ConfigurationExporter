using System.Configuration;

namespace AspNet.Mvc.ConfigurationExporter.Section
{
    public class ConfigrSectionHandler : ConfigurationSection, IConfigrSectionHandler
    {

        [ConfigurationProperty("appSettings")]
        public AppSettingsCollection AppSettings
        {
            get
            {
                return this["appSettings"] as AppSettingsCollection;
            }
        }

        public static ConfigrSectionHandler GetConfig()
        {
            return ConfigurationManager.GetSection("exposeConfigr") as ConfigrSectionHandler;
        }
    }
}