namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public class ContaPoupanca : ContaBancaria
    {
        protected internal ContaPoupanca()
        {
            this.AceitaSaldoNegativo = false;
        }
    }
}