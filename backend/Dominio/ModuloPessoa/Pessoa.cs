using System.Collections.Generic;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Pessoa : EntidadeBase
    {
        protected internal Pessoa() { }

        internal Pessoa(string nome, string cpf, string email)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Email = email;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual IList<Trabalho> Trabalhos { get; set; } = new List<Trabalho>();

        public void AdicionarTrabalho(Trabalho trabalho)
        {
            trabalho.Pessoa = this;
            this.Trabalhos.Add(trabalho);
        }

    }
}
