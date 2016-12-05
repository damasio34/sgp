using System.Collections.Generic;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Usuario : EntidadeBase
    {
        protected internal Usuario() { }
        protected internal Usuario(string login, string senha)
        {
            this.Login = login;
            this.Senha = senha;
        }

        public string Login { get; set; }
        public string Senha { get; set; }
        public virtual IList<Trabalho> Trabalhos { get; set; } = new List<Trabalho>();        

        public void AdicionarTrabalho(Trabalho trabalho)
        {
            trabalho.Usuario = this;           
            this.Trabalhos.Add(trabalho);            
        }
    }
}