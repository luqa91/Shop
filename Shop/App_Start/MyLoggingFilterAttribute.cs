

using NLog;
using System.Web.Mvc;

namespace Shop.App_Start
{
    public class MyLoggingFilter: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logger logger = LogManager.GetCurrentClassLogger();
            var request = filterContext.RequestContext;
            logger.Warn(request);





            filterContext.HttpContext.Trace.Write("(Logging Filter)Action Executing: " +
                filterContext.ActionDescriptor.ActionName);

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null)
                filterContext.HttpContext.Trace.Write("(Logging Filter)Exception thrown");

            base.OnActionExecuted(filterContext);
        }
    



    }
}