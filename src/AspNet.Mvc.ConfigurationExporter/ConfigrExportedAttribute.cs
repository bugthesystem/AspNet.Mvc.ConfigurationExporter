using System;

namespace AspNet.Mvc.ConfigurationExporter
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ConfigrExportedAttribute : Attribute
    {
        public string Name { get; set; }
    }
}