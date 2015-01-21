namespace AspNet.Mvc.ConfigurationExporter
{
    public interface IExposeModeDetector
    {
        SettingsExposeMode Detect();
    }
}