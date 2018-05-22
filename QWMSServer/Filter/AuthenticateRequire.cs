using QWMSServer.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QWMSServer.Filter
{
    public class AuthenticateRequire : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            if (!filterContext.Request.Headers.Contains("Token"))
            {
                filterContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                base.OnActionExecuting(filterContext);
                return;
            }
            var requestScope = filterContext.Request.GetDependencyScope();
            var _authService = requestScope.GetService(typeof(IAuthService)) as IAuthService;

            var tokenValue = filterContext.Request.Headers.GetValues("Token").First();

            var validateResult = _authService.ValidateToken(tokenValue);
            var isValid = validateResult;
            if (!isValid)
            {
                var responseMessage = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "Invalid Request" };
                filterContext.Response = responseMessage;
            }

            //var token = validateResult.Item2;
            //filterContext.Request.Properties.Add("Token", token);
            base.OnActionExecuting(filterContext);
        }
    }
}