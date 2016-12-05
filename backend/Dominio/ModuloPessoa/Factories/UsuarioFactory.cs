using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa.Factories
{
    public static class UsuarioFactory
    {
        public static Usuario Criar(Pessoa pessoa, string login, string senha)
        {
            var usuario = new Usuario(pessoa, login, senha);
            usuario.GerarId();

            return usuario;
        }
    }
}
