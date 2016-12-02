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
                var user = context.UserName;
                var password = context.Password;

                var _repository = new UsuarioRepository(new MainUnitOfWork());

                if (!_repository.Existe(p => p.Login.Equals(user) && p.Senha.Equals(password)))
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user));

                var roles = new List<string> { "User" };

                //foreach (var role in roles) identity.AddClaim(new Claim(ClaimTypes.Role, role));

                var principal = new GenericPrincipal(identity, roles.ToArray());
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