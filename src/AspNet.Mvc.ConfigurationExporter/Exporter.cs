using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class Exporter
    {
        private static readonly Lazy<Exporter> ExportLazy = new Lazy<Exporter>(() => new Exporter(), true);
        protected internal readonly ConcurrentDictionary<Type, Tuple<BindingFlags, Func<Type, object>>> Exports;
        public Exporter()
        {
            Exports = new ConcurrentDictionary<Type, Tuple<BindingFlags, Func<Type, object>>>();
        }
        public static Exporter Instance => ExportLazy.Value;

        public void RegisterType<T>(Func<Type, object> resolver,
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static)

        {
            if (!Exports.ContainsKey(typeof(T)))
            {
                Exports.TryAdd(typeof(T), new Tuple<BindingFlags, Func<Type, object>>(flags, resolver));
            }
        }
    }
}