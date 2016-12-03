using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloTrabalho;
using Damasio34.SGP.Dominio.ModuloTrabalho.Factories;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Usuario : EntidadeBase
    {
        protected internal Usuario() { }
        protected internal Usuario(string login, string senha, Trabalho trabalho)
        {
            this.Login = login;
            this.Senha = senha;
            this.AdicionarTrabalho(trabalho);
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