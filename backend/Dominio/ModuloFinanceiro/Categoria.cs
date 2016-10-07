using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public class Categoria : EntidadeBase
    {
        protected internal Categoria() { }

        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}