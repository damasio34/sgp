namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Irrf : Imposto
    {
        public Irrf(double valor) : base(TipoDoImposto.Irrf) { this.CalcularAliquota(valor); }

        protected sealed override void CalcularAliquota(double valor)
        {            
            if (valor <= 1903.98) this.Valor = 0;
            else if (valor >= 1903.99 && valor < 2826.65) this.Valor = (0.075 * valor) - 142.80;
            else if (valor >= 2826.66 && valor < 3751.05) this.Valor = (0.15 * valor) - 354.80;
            else if (valor >= 3751.06 && valor < 4664.68) this.Valor = (0.225 * valor) - 636.13;
            else this.Valor = (0.275 * valor) - 869.36;
        }
    }
}