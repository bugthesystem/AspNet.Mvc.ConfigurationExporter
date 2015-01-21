using System;
using System.Configuration;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ExposeModeDetector : IExposeModeDetector
    {
        public SettingsExposeMode Detect()
        {
            SettingsExposeMode result = SettingsExposeMode.Keys;

            string modeSettings = ConfigurationManager.AppSettings.Get(Contants.ModeKey);

            if (!string.IsNullOrEmpty(modeSettings))
            {
                Enum.TryParse(modeSettings, true, out result);
            }

            return result;
        }
    }
}