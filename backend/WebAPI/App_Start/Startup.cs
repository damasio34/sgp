using System;
using System.Web.Http;
using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Aplicacao.Services;
using Damasio34.SGP.Data.Repositories.ModuloPessoa;
using Damasio34.SGP.Data.Repositories.ModuloTrabalho;
using Damasio34.SGP.Data.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;
using Damasio34.SGP.WebAPI.Autenticacao;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(Damasio34.SGP.WebAPI.Startup))]
namespace Damasio34.SGP.WebAPI
{    
    public class Startup
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();

            container.RegisterWebApiRequest<IUnitOfWork, MainUnitOfWork>();

            // AppServices
            container.RegisterWebApiRequest<ITrabalhoRepository, TrabalhoRepository>();
            container.RegisterWebApiRequest<IUsuarioRepository, UsuarioRepository>();

            // Repositórios
            container.RegisterWebApiRequest<ITrabalhoAppService, TrabalhoAppService>();

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);            
        }

        public void Configuration(IAppBuilder app)
        {
            
            var config = new HttpConfiguration();
            ConfigureWebApi(config);
            Register(config);
            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);
        }

        public static void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults:new { id = RouteParameter.Optional }
            );
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            var oAuthServerOptions = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/security/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(2),
                Provider = new AuthorizationServerProvider()
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

}