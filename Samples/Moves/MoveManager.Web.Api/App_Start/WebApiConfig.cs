using System.Web.Http;
using MoveManager.Web.Api.App_Start;

namespace MoveManager.Web.Api
{
    public static class WebApiConfig
    {
        public static HttpConfiguration Register()
        {
            var config = new HttpConfiguration();

            Register(config);

            return config;
        }

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.DependencyResolver = IoC.DependencyResolver;

            // Web API routes
            config.MapHttpAttributeRoutes();
        }
    }
}