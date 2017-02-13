using System;

namespace AspNet.Mvc.ConfigurationExporter
{
    [Obsolete("Renamed, please use ConfigrExportableAttribute", true)]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ConfigrExportedAttribute : Attribute
    {
        public string Name { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
    public class ConfigrExportableAttribute : Attribute
    {
        public string Name { get; set; }
    }
}