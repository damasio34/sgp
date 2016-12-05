namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public abstract class Imposto : IImposto
    {
        protected Imposto(TipoDoImposto tipoDoImposto)
        {
            this.TipoDoImposto = tipoDoImposto;
        }

        public TipoDoImposto TipoDoImposto { get; protected set; }
        public double Valor { get; protected set; }

        protected abstract void CalcularAliquota(double valor);
    }
}