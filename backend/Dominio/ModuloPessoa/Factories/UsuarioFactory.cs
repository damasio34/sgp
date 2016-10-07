using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damasio34.SGP.Dominio.ModuloPessoa.Factories
{
    public static class UsuarioFactory
    {
        public static Usuario Criar(string login, string senha)
        {
            var usuario = new Usuario();
            usuario.Login = login;
            usuario.Senha = senha;

            return usuario;
        }
    }
}
