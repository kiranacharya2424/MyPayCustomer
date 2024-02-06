using MyPay.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiThrottle;

namespace MyPay.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var throttleFilter = new ThrottlingFilter
            {
                Policy = new ThrottlePolicy(perSecond: 10, perMinute: 50, perHour: 60, perDay: 600)
                {
                    IpThrottling = true
                },
                Repository = new CacheRepository()
            };
            config.EnableCors();
            config.Filters.Add(new BasicAuthenticationAttribute());
            config.Filters.Add(new AuthorizeAttribute());
            config.Filters.Add(throttleFilter);

            //var cors = new EnableCorsAttribute("*", "*", "*");// origins, headers, methods  
            //config.EnableCors(cors);


            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

        }
    }
}
