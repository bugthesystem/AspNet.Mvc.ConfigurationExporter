using System;
using System.Web;
using System.Web.Routing;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ConfigrHandler : IHttpHandler
    {
      

        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly IScriptBuilder _scriptBuilder;
        private readonly IConfigrSettingsSerializer _settingsSerializer;

        public ConfigrHandler(RequestContext requestContext, IConfigrSettingsSerializer settingsSerializer,
            IAppSettingsProvider appSettingsProvider, IScriptBuilder scriptBuilder)
        {
            _settingsSerializer = settingsSerializer;
            _appSettingsProvider = appSettingsProvider;
            _scriptBuilder = scriptBuilder;
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
                string js = _scriptBuilder.Build(settingsJsonString);
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