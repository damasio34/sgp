using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Usuario : EntidadeBase
    {
        internal Usuario() { }
        internal Usuario(Pessoa pessoa, string login, string senha)
        {
            this.Pessoa = pessoa;

            this.Login = login;
            this.Senha = senha;
        }

        public virtual Pessoa Pessoa { get; private set; }

        public string Login { get; set; }
        public string Senha { get; set; }        
    }
}