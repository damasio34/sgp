namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public class Carteira : Conta
    {
        protected internal Carteira()
        {
            this.AceitaSaldoNegativo = false;
        }
    }
}