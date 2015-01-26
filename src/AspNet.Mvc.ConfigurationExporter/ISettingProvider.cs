using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNet.Mvc.ConfigurationExporter
{
    public interface ISettingProvider
    {
        SettingsExposeMode GetExposeMode();

        string GetNamespace();
    }
}
