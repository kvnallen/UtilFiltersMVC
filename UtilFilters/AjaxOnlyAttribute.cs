using System;
using System.Web.Mvc;

namespace UtilFilters
{

    /// <summary>
    /// Allow only ajax requests
    /// </summary>
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
                throw new Exception("Only Ajax requests are allowed.");
        }
    }
}