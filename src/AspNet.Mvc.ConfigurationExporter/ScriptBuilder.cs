using System;
using System.Linq;
using System.Text;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ScriptBuilder : IScriptBuilder
    {
        private readonly IAppSettingsProvider _appSettingsProvider;

        public ScriptBuilder(IAppSettingsProvider appSettingsProvider)
        {
            _appSettingsProvider = appSettingsProvider;
        }

        public string GetUserDefinedNamespace()
        {
            var result = new StringBuilder();

            string ns = _appSettingsProvider.GetNamespace();

            if (String.IsNullOrEmpty(ns))
            {
                return result.ToString();
            }

            // Get array of objects we need to create on client side.
            string[] parts = ns.Split('.');

            // Check if we ahve any object names.
            if (parts.Length == 0)
            {
                // Return empty string.
                return result.ToString();
            }

            for (int i = 0; i < parts.Length; i++)
            {
                // Get first required part of namespace.
                string tmp = String.Join(".", parts.Skip(0).Take(i + 1).ToArray());

                // Add namespace decleration to result.
                result.AppendFormat(i == parts.Length - 1 ? "{0} = window.{0} || " : "{0} = window.{0} || {{}};", tmp);
            }

            return result.ToString();
        }
    }
}