namespace AspNet.Mvc.ConfigurationExporter.Section
{
    public interface IConfigrSectionHandler
    {
        AppSettingsCollection AppSettings { get; }
    }
}