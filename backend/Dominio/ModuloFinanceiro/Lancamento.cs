using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public class Lancamento : EntidadeBase
    {
        protected internal Lancamento() { }
        protected internal Lancamento(double valor, TipoLancamento tipoLancamento, string descricao)
        {            
            this.TipoLancamento = tipoLancamento;
            this.Descricao = descricao;

            if (this.TipoLancamento.Equals(TipoLancamento.Saida) && valor > 0) this.Valor = valor * -1;
            else this.Valor = valor;
        }

        public string Descricao { get; set; }
        public double Valor { get; set; }
        public TipoLancamento TipoLancamento { get; set; }
    }
}