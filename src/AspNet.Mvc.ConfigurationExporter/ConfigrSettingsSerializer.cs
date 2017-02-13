using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
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
            var appSettingsDictionary = new Dictionary<string, string>();

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

            SerializeExports(appSettingsDictionary);

            return JsonConvert.SerializeObject(appSettingsDictionary);
        }

        private void SerializeExports(Dictionary<string, string> appSettingsDictionary)
        {
            foreach (KeyValuePair<Type, Tuple<BindingFlags, Func<Type, object>>> export in Exporter.Instance.Exports)
            {
                object instance = export.Value.Item2(export.Key);
                if (instance != null)
                {
                    PropertyInfo[] propertyInfos = instance.GetType().GetProperties(export.Value.Item1);
                    foreach (PropertyInfo property in propertyInfos)
                    {
                        ConfigrExportableAttribute attribute = property.GetCustomAttribute<ConfigrExportableAttribute>();

                        if (attribute != null)
                        {
                            object value = property.GetValue(instance, null);

                            if (value != null)
                            {
                                appSettingsDictionary.Add(!string.IsNullOrEmpty(attribute.Name) ? attribute.Name : property.Name, value.ToString());
                            }
                        }
                    }
                }
            }
        }

        private Dictionary<string, string> SerializeFromSection()
        {
            return _configrSection.AppSettings.Cast<AppSettings>().ToDictionary(settings => settings.Key, settings => settings.Value);
        }

        private Dictionary<string, string> SerializeFromKeys(string configKey)
        {
            var result = new Dictionary<string, string>();

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