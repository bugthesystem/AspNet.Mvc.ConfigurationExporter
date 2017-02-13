using System.Configuration;

namespace AspNet.Mvc.ConfigurationExporter.Section
{
    public class AppSettings : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key => this["key"] as string;

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value => this["value"] as string;
    }
}