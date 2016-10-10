namespace Damasio34.SGP.Dominio.ModuloPessoa.Factories
{
    public static class UsuarioFactory
    {
        public static Usuario Criar(string login, string senha)
        {
            var usuario = new Usuario
            {
                Login = login,
                Senha = senha
            };
            usuario.GerarId();

            return usuario;
        }
    }
}
