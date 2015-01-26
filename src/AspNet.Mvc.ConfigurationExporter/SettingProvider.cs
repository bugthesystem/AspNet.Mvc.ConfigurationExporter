using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class SettingProvider : ISettingProvider
    {
        public SettingsExposeMode GetExposeMode()
        {
            SettingsExposeMode result = SettingsExposeMode.Keys;

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
