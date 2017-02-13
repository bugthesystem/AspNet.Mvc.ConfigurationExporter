namespace AspNet.Mvc.ConfigurationExporter.WebSite
{
    public interface ITestConfiguration
    {
        int Db { get; }

        string AppUrl { get; }

        int Test { get; }
    }

    public class TestConfiguration : ITestConfiguration
    {
        public int Db { get { return 20; } }

        [ConfigrExportable]
        public string AppUrl { get { return "https://github.com/PanteonProject"; } }

        [ConfigrExportable(Name = "testName")]
        public int Test { get { return 10; } }

    }
}