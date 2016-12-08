using System.Linq;
using System.Security.Claims;

namespace Damasio34.SGP.Aplicacao.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static bool IsAuthenticated(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(a => a.Type.Contains("hasRegistered") || a.Type.Contains("nameidentifier")) != null;
        }
    }
}
