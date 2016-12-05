using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa.Factories
{
    public static class PessoaFactory
    {
        public static Pessoa Criar(string nome, string cpf, string email)
        {
            var pessoa = new Pessoa(nome, cpf, email);
            pessoa.GerarId();

            return pessoa;
        }
    }
}
