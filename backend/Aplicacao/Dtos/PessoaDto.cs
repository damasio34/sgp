using System;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Dtos
{
    public class PessoaDto
    {
        // Dados da pessoa
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        // Dados do usuário
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
