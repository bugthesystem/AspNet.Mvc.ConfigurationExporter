using System;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ConfigrHandler : IHttpHandler
    {
        private const string ScriptTemplate = @";(function(){{ {0} JSON.parse('{1}'); }})();";
        private const string DefaultNamespaceScript = @"window.configuration = window.configuration || ";

        private readonly IConfigrSettingsSerializer _settingsSerializer;
        private readonly ISettingProvider _settingProvider;
        public RequestContext RequestContext { get; private set; }

        public ConfigrHandler(RequestContext requestContext, IConfigrSettingsSerializer settingsSerializer, ISettingProvider settingProvider)
        {
            _settingsSerializer = settingsSerializer;
            _settingProvider = settingProvider;
            RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            SettingsExposeMode mode = _settingProvider.GetExposeMode();
            string settingsJsonString = _settingsSerializer.Serialize(mode);
            if (!string.IsNullOrEmpty(settingsJsonString))
            {
                context.Response.Clear();
                context.Response.ContentType = "text/javascript";
                string userDefinedNamespaceScript = GetUserDefinedNamespaceScript();
                string js = string.Format(ScriptTemplate, String.IsNullOrEmpty(userDefinedNamespaceScript) ? DefaultNamespaceScript : userDefinedNamespaceScript, settingsJsonString);
                context.Response.Write(js);
                context.Response.End();
            }
        }

        public virtual bool IsReusable
        {
            get
            {
                return true;
            }
        }

        #region Helpers

        private string GetUserDefinedNamespaceScript()
        {
            // Get namespace.
            string ns = _settingProvider.GetNamespace();

            // Validate namespace.
            if (String.IsNullOrEmpty(ns))
            {
                // Return empty string.
                return String.Empty;
            }

            // Get array of objects we need to create on client side.
            string[] objects = ns.Split('.');

            // Check if we ahve any object names.
            if (objects == null || objects.Length == 0)
            {
                // Return empty string.
                return String.Empty;
            }

            // Initialize result string.
            string result = String.Empty;

            for (int i = 0; i < objects.Length; i++)
            {
                // Get first required part of namespace.
                string temp = String.Join(".", objects.Skip(0).Take(i + 1).ToArray());

                // Add namespace decleration to result.
                if (i == objects.Length - 1)
                {
                    result += String.Format("{0} = window.{0} || ", temp);
                }
                else
                {
                    result += String.Format("{0} = window.{0} || {{}};", temp);
                }
            }

            // Return result.
            return result;
        }

        #endregion
    }
}