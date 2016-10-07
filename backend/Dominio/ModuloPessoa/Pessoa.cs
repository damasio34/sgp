using System.Collections.Generic;
using Damasio34.Seedwork.Domain;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Dominio.ModuloPessoa
{
    public class Pessoa : EntidadeBase
    {        
        public string Nome { get; set; }

        public virtual Usuario Usuario { get; set; }

        public virtual ICollection<Trabalho> Emprego { get; set; }
    }
}
