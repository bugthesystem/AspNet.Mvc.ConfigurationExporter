namespace AspNet.Mvc.ConfigurationExporter
{
    public interface IConfigrSettingsSerializer
    {
        string Serialize(SettingsExposeMode mode, string configKey = null);
    }
}