using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MoveManager.Web.Api.App_Start.Startup))]

namespace MoveManager.Web.Api.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit 
            app.UseWebApi(WebApiConfig.Register());
        }
    }
}
