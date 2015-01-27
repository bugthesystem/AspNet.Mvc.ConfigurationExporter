namespace AspNet.Mvc.ConfigurationExporter
{
    public interface IScriptBuilder
    {
        string Build(string json);
    }
}