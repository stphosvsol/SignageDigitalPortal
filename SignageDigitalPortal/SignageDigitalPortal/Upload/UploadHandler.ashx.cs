using System.IO;
using System.Web;
using System.Web.Script.Serialization;
using SignageDigitalPortal.Services.Upload;

namespace SignageDigitalPortal.Upload
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler
    {

        private readonly JavaScriptSerializer _js;
        private string UrlApp
        {
            get { return HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath; }
        }

        private string MediaStorage
        {
            get { return Path.Combine(HttpContext.Current.Server.MapPath("~/"), UploadMediaService.MediaRelativePath); }
        }


        public UploadHandler()
        {
            _js = new JavaScriptSerializer {MaxJsonLength = 41943040};
        }

        public bool IsReusable
        {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.AddHeader("Pragma", "no-cache");
            context.Response.AddHeader("Cache-Control", "private, no-cache");

            HandleMethod(context);
        }

        // Handle request based on method
        private void HandleMethod(HttpContext context)
        {
            switch (context.Request.HttpMethod)
            {
                case "POST":
                case "PUT":
                    if (context.Request.QueryString["upFile"] == "1")
                    {
                        UploadMediaFile(context);
                    }
                    else
                    {
                        context.Response.ClearHeaders();
                        context.Response.StatusCode = 405;
                    }
                    break;
                default:
                    context.Response.ClearHeaders();
                    context.Response.StatusCode = 405;
                    break;
            }
        }

        private void UploadMediaFile(HttpContext context)
        {
            var service = new UploadMediaService(MediaStorage, UrlApp, UploadMediaService.MediaRelativePath);
            WriteJsonIframeSafe(context, service.ValidateAndSaveMedia(context));
        }


        private void WriteJsonIframeSafe(HttpContext context, object response)
        {
            context.Response.AddHeader("Vary", "Accept");
            try
            {
                context.Response.ContentType = context.Request["HTTP_ACCEPT"].Contains("application/json") ? "application/json" : "text/plain";
            }
            catch
            {
                context.Response.ContentType = "text/plain";
            }

            var jsonObj = _js.Serialize(response);
            context.Response.Write(jsonObj);
        }
    }
}

