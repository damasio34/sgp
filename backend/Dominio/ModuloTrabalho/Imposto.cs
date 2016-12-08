using Damasio34.Seedwork.Domain;

namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public abstract class Imposto : EntidadeBase, IImposto
    {
        #region [ Construtores ]
        
        protected Imposto(TipoDoImposto tipoDoImposto)
        {
            this.TipoDoImposto = tipoDoImposto;
        }

        #endregion

        #region [ Propriedades ]        

        public TipoDoImposto TipoDoImposto { get; protected set; }
        public double Valor { get; protected set; }

        #endregion

        #region [ M�todos ]        

        protected abstract void CalcularAliquota(double valor);

        #endregion
    }
}