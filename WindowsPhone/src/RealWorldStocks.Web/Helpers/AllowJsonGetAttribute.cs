using System.Web.Mvc;

namespace RealWorldStocks.Web.Helpers
{
    public class AllowJsonGetAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = filterContext.Result as JsonResult;
            if(result != null)
            {
                result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
        }
    }
}