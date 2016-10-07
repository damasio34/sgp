using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace Damasio34.SGP.WebAPI
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
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new []{ "*" });

            try
            {
                var user = context.UserName;
                var password = context.Password;

                if (user != "damasio34" || password != "12345")
                {
                    context.SetError("invalid_grant", "Usuário ou senha inválidos");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim(ClaimTypes.Name, user));

                var roles = new List<string> {"User"};

                foreach (var role in roles) identity.AddClaim(new Claim(ClaimTypes.Role, role));                

                var principal = new GenericPrincipal(identity, roles.ToArray());
                Thread.CurrentPrincipal = principal;

                context.Validated(identity);                
            }
            catch (Exception)
            {                
                context.SetError("invalid_grant", "Falha ao autenticar");
            }
        }
    }
}