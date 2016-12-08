using System;
using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Usuario : EntidadeBase
    {
        internal Usuario() { }
        internal Usuario(Pessoa pessoa, string login, string senha)
        {
            pessoa.Usuario = this;

            this.Pessoa = pessoa;
            this.Login = login;
            this.Senha = senha;
        }

        public virtual Pessoa Pessoa { get; protected set; }

        public string Login { get; set; }
        public string Senha { get; set; }        
    }
}