using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa.Factories
{
    public static class UsuarioFactory
    {
        public static Usuario Criar(string login, string senha, Trabalho trabalho = null)
        {
            var usuario = new Usuario(login, senha, trabalho);
            usuario.GerarId();

            return usuario;
        }
    }
}
