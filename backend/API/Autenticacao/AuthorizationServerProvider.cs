using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Damasio34.SGP.Data.Repositories.ModuloPessoa;
using Damasio34.SGP.Data.UnitOfWork;
using Microsoft.Owin.Security.OAuth;

namespace Damasio34.SGP.API.Autenticacao
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            // ToDo: Corrigir
            return base.ValidateClientAuthentication(context);
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            try
            {
                var login = context.UserName;
                var senha = context.Password;
                var repository = new UsuarioRepository(new MainUnitOfWork());

                if (!repository.Existe(p => p.Login.Equals(login) && p.Senha.Equals(senha)))
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                var roles = new List<string> { "Usuario" };          
                var principal = new GenericPrincipal(identity, roles.ToArray());

                identity.AddClaim(new Claim(ClaimTypes.Name, login));
                Thread.CurrentPrincipal = principal;
                context.Validated(identity);
            }
            catch (Exception ex)
            {
                context.SetError("invalid_grant", "Falha ao autenticar");
            }
        }
    }
}