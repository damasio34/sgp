using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Usuario : EntidadeBase
    {
        protected internal Usuario() { }
        public string Login { get; set; }
        public string Senha { get; set; }  
    }
}