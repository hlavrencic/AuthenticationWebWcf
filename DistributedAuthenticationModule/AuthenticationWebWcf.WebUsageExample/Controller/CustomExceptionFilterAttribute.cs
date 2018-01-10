using System.Web.Http.Filters;

namespace AuthenticationWebWcf.WebUsageExample.Controller
{
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {
        #region Overrides of ExceptionFilterAttribute

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);
        }

        #endregion
    }
}
