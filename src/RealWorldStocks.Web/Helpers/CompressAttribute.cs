using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace RealWorldStocks.Web.Helpers
{
    public class CompressAttribute : ActionFilterAttribute
    {
        //public override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    if (filterContext.Exception != null)
        //        return;

        //    HttpResponseBase response = filterContext.HttpContext.Response;

        //    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
        //    response.AppendHeader("Content-encoding", "gzip");

        //    base.OnResultExecuted(filterContext);
        //}



        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var response = filterContext.HttpContext.Response;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToLowerInvariant();

            if (acceptEncoding.Contains("gzip"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("deflate"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }
}   