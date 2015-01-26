using System;
using System.Configuration;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class AppSettingsProvider : IAppSettingsProvider
    {
        public SettingsExposeMode GetExposeMode()
        {
            var result = SettingsExposeMode.Keys;

            string modeSettings = ConfigurationManager.AppSettings.Get(Contants.ModeKey);

            if (!string.IsNullOrEmpty(modeSettings))
            {
                Enum.TryParse(modeSettings, true, out result);
            }

            return result;
        }

        public string GetNamespace()
        {
            return ConfigurationManager.AppSettings.Get(Contants.NamespaceKey);
        }
    }
}