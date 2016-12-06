using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;

namespace Damasio34.SGP.Dominio.ModuloPessoa.Factories
{
    public static class PessoaFactory
    {
        public static Pessoa Criar(string nome, string cpf, string email, Trabalho trabalho = null)
        {
            var pessoa = new Pessoa(nome, cpf, email);
            pessoa.GerarId();

            if (trabalho.IsNull()) trabalho = TrabalhoFactory.Criar(pessoa);
            pessoa.AdicionarTrabalho(trabalho);

            return pessoa;
        }
    }
}
