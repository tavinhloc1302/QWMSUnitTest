using QWMSServer.Data.Services;
using QWMSServer.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Reflection;

namespace QWMSServer.Filter
{
    public class LogRequire : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var resutl = actionExecutedContext.Response.Content.ReadAsAsync(
                actionExecutedContext.ActionContext.ActionDescriptor.ReturnType);
            //var logViewModel = resutl.R;

            //Get Device status
            //Create log record respone.Result.Content.ReadAsAsync<ResponseViewModel<DOViewModel>>();
            var resutl2 = actionExecutedContext.Response.Content.ReadAsAsync<ResponseViewModel<object>>();
            var log = resutl2.Result.logViewModel;
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}