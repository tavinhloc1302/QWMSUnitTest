using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace QWMSServer
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // JSON dumps configs
            var _jsonConfig = config.Formatters.JsonFormatter;
            _jsonConfig.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            _jsonConfig.UseDataContractJsonSerializer = false;
            _jsonConfig.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
