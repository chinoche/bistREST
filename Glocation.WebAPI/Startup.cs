using System;
using System.Configuration;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using log4net.Config;

[assembly: OwinStartup(typeof(Glocation.WebAPI.Startup))]
namespace Glocation.WebAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            XmlConfigurator.Configure();
            var config = new HttpConfiguration();
            ConfigureOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        private static void ConfigureOAuth(IAppBuilder app)
        {
            //TODO
            //Configure the auth service
 
        }

    }
}
