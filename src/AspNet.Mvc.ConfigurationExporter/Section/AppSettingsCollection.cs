using System.Configuration;

namespace AspNet.Mvc.ConfigurationExporter.Section
{
    public class AppSettingsCollection : ConfigurationElementCollection
    {
        public AppSettings this[int index]
        {
            get
            {
                return BaseGet(index) as AppSettings;
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new AppSettings();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AppSettings)element).Key;
        }
    }
}