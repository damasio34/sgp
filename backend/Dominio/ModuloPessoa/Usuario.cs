using System.Collections.Generic;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Usuario : EntidadeBase
    {
        protected internal Usuario() { }
        public string Login { get; set; }
        public string Senha { get; set; }
        public ICollection<Trabalho> Trabalhos { get; set; }
    }
}