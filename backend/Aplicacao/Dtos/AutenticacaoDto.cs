using System;
using System.Linq;
using System.Security.Claims;
using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Aplicacao.Dtos
{
    public class AutenticacaoDto : IAutenticacao
    {
        private ClaimsPrincipal _principal;      

        private AutenticacaoDto() { }
        private AutenticacaoDto(ClaimsPrincipal principal)
        {
            this._principal = principal;
            var claimIdUsuario = principal.Claims.FirstOrDefault(a => a.Type.Contains("nameidentifier"));
            var claimLogin = principal.Claims.FirstOrDefault(a => a.Type.Contains("name"));

            if (claimIdUsuario != null) IdUsuario = Guid.Parse(claimIdUsuario.Value);
            if (claimLogin != null) Login = claimLogin.Value;

            IsAutheticated = true;
        }

        public bool IsAutheticated { get; set; }
        public Guid IdUsuario { get; set; }
        public Guid IdGrupo { get; set; }
        public string Login { get; set; }

        public static AutenticacaoDto NotAuthenticated()
        {
            return new AutenticacaoDto { IsAutheticated = false };
        }
        public static AutenticacaoDto IsAuthenticated(ClaimsPrincipal principal)
        {
            return new AutenticacaoDto(principal);
        }
    }
}
