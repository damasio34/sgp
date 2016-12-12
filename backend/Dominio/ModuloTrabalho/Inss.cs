namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public class Inss : Imposto
    {
        public Inss(double valor) : base(TipoDoImposto.INSS) { this.CalcularAliquota(valor); }

        protected sealed override void CalcularAliquota(double valor)
        {
            if (valor <= 1556.94) this.Valor = valor * 0.08;
            else if (valor >= 1556.95 && valor < 2594.92) this.Valor = valor * 0.09;
            else if (valor >= 1556.95 && valor < 2594.92) this.Valor = valor * 0.09;
            else if (valor >= 2594.93 && valor < 5189.82) this.Valor = valor * 0.11;
            else this.Valor = 570.88;
        }
    }
}