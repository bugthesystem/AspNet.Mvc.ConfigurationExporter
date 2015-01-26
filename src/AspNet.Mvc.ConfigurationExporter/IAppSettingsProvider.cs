namespace AspNet.Mvc.ConfigurationExporter
{
    public interface IAppSettingsProvider
    {
        SettingsExposeMode GetExposeMode();

        string GetNamespace();
    }
}