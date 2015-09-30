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
        [ConfigrExported]
        public string AppUrl { get { return "https://github.com/PanteonProject"; } }

        [ConfigrExported(Name = "testName")]
        public int Test { get { return 10; } }

    }
}