using System;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Aplicacao.Interfaces;
using Damasio34.SGP.Aplicacao.Services;
using Damasio34.SGP.API;
using Damasio34.SGP.Data.Repositories.ModuloPessoa;
using Damasio34.SGP.Data.UnitOfWork;
using Damasio34.SGP.Dominio.ModuloPessoa.Interfaces;
using Damasio34.SGP.API.Autenticacao;
using Damasio34.Seedwork.UnitOfWork;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Aplicacao.Extensions;
using Damasio34.SGP.Data.Repositories.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Interfaces;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

[assembly: OwinStartup(typeof(WebApiConfig))]
namespace Damasio34.SGP.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var container = new Container();

            Func<IAutenticacao> getAutenticacao = () =>
            {
                var principal = Thread.CurrentPrincipal as ClaimsPrincipal;
                var autenticacao = principal.IsAuthenticated() ? AutenticacaoDto.IsAuthenticated(principal) : AutenticacaoDto.NotAuthenticated();

                return autenticacao;
            };

            container.RegisterWebApiRequest<IUnitOfWork, MainUnitOfWork>();
            container.RegisterWebApiRequest(getAutenticacao);

            // Repositórios
            container.RegisterWebApiRequest<IUsuarioRepository, UsuarioRepository>();
            container.RegisterWebApiRequest<IPessoaRepository, PessoaRepository>();
            container.RegisterWebApiRequest<ITrabalhoRepository, TrabalhoRepository>();

            // AppServices
            container.RegisterWebApiRequest<IPessoaAppService, PessoaAppService>();
            container.RegisterWebApiRequest<ITrabalhoAppService, TrabalhoAppService>();

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
        public static void Configuration(IAppBuilder app)
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
            var cors = new System.Web.Http.Cors.EnableCorsAttribute(
            origins: "*",
            headers: "*",
            methods: "*");
            config.EnableCors(cors);

            config.MapHttpAttributeRoutes();
            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
        public static void ConfigureOAuth(IAppBuilder app)
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
