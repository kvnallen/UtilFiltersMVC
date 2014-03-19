using System.IO.Compression;
using System.Web.Mvc;

namespace UtilFilters
{

    /// <summary>
    /// If server supports, encode the content
    /// to gzip / deflate
    /// </summary>
    public class CompressFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {  
            base.OnActionExecuting(filterContext);
            
            var response = filterContext.HttpContext.Response;
            if (response.Filter is GZipStream || response.Filter is DeflateStream) return;

            var request = filterContext.HttpContext.Request;
            var acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToLower();

            if (response.Filter != null && acceptEncoding.Contains("gzip"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (response.Filter != null && acceptEncoding.Contains("deflate"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}