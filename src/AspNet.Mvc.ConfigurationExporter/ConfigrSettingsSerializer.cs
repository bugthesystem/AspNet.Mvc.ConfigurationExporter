using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AspNet.Mvc.ConfigurationExporter.Section;
using Newtonsoft.Json;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ConfigrSettingsSerializer : IConfigrSettingsSerializer
    {
        private readonly IConfigrSectionHandler _configrSection;

        public ConfigrSettingsSerializer(IConfigrSectionHandler configrSection)
        {
            _configrSection = configrSection;
        }

        public string Serialize(SettingsExposeMode mode, string configKey = null)
        {
            Dictionary<string, string> appSettingsDictionary = new Dictionary<string, string>();

            switch (mode)
            {
                case SettingsExposeMode.Keys:
                    if (string.IsNullOrEmpty(configKey))
                    {
                        configKey = Contants.KeysArrayKey;
                    }
                    appSettingsDictionary = SerializeFromKeys(configKey);
                    break;
                case SettingsExposeMode.Section:
                    appSettingsDictionary = SerializeFromSection();
                    break;
            }

            return JsonConvert.SerializeObject(appSettingsDictionary);
        }

        private Dictionary<string, string> SerializeFromSection()
        {
            return _configrSection.AppSettings.Cast<AppSettings>().ToDictionary(settings => settings.Key, settings => settings.Value);
        }

        private Dictionary<string, string> SerializeFromKeys(string configKey)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            string keysToSerialize = ConfigurationManager.AppSettings.Get(configKey);
            if (!string.IsNullOrEmpty(keysToSerialize))
            {
                string[] keys = keysToSerialize.Split('|');
                foreach (string key in keys)
                {
                    string settings = ConfigurationManager.AppSettings.Get(key);
                    if (!string.IsNullOrEmpty(settings))
                    {
                        result.Add(key, settings);
                    }
                }
            }

            return result;
        }
    }
}