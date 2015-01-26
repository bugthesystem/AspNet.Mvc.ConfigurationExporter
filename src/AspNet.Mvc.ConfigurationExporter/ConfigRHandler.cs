using System;
using System.Web;
using System.Web.Routing;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ConfigrHandler : IHttpHandler
    {
        private const string ScriptTemplate = @";(function(){{ {0} JSON.parse('{1}'); }})();";
        private const string DefaultNamespaceScript = @"window.configuration = window.configuration || ";

        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IScriptHelper _scriptHelper;
        private readonly IConfigrSettingsSerializer _settingsSerializer;

        public ConfigrHandler(RequestContext requestContext, IConfigrSettingsSerializer settingsSerializer,
            IAppSettingsProvider appSettingsProvider, IScriptHelper scriptHelper)
        {
            _settingsSerializer = settingsSerializer;
            _appSettingsProvider = appSettingsProvider;
            _scriptHelper = scriptHelper;
            RequestContext = requestContext;
        }

        public RequestContext RequestContext { get; private set; }

        public void ProcessRequest(HttpContext context)
        {
            SettingsExposeMode mode = _appSettingsProvider.GetExposeMode();
            string settingsJsonString = _settingsSerializer.Serialize(mode);

            if (!string.IsNullOrEmpty(settingsJsonString))
            {
                context.Response.Clear();
                context.Response.ContentType = "text/javascript";
                string userDefinedNamespace = _scriptHelper.GetUserDefinedNamespace();
                string js = string.Format(ScriptTemplate,
                    String.IsNullOrEmpty(userDefinedNamespace) ? DefaultNamespaceScript : userDefinedNamespace,
                    settingsJsonString);
                context.Response.Write(js);
                context.Response.End();
            }
        }

        public virtual bool IsReusable
        {
            get { return true; }
        }
    }
}