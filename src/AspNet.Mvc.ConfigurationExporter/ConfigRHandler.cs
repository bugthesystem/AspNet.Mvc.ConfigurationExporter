using System.Web;
using System.Web.Routing;

namespace AspNet.Mvc.ConfigurationExporter
{
    public class ConfigrHandler : IHttpHandler
    {
        private const string JsTmpl = @";(function(){{ if(!window.configuration){{ window.configuration = JSON.parse('{0}'); }} }})();";

        private readonly IConfigrSettingsSerializer _settingsSerializer;
        private readonly IExposeModeDetector _exposeModeDetector;
        public RequestContext RequestContext { get; private set; }

        public ConfigrHandler(RequestContext requestContext, IConfigrSettingsSerializer settingsSerializer, IExposeModeDetector exposeModeDetector)
        {
            _settingsSerializer = settingsSerializer;
            _exposeModeDetector = exposeModeDetector;
            RequestContext = requestContext;
        }

        public void ProcessRequest(HttpContext context)
        {
            SettingsExposeMode mode = _exposeModeDetector.Detect();
            string settingsJsonString = _settingsSerializer.Serialize(mode);
            if (!string.IsNullOrEmpty(settingsJsonString))
            {
                context.Response.Clear();
                context.Response.ContentType = "text/javascript";
                string js = string.Format(JsTmpl, settingsJsonString);
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
    }
}