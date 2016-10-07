namespace Damasio34.SGP.Dominio.ModuloFinanceiro.Factories
{
    public static class ContaFactory
    {
        public static ContaCorrente Criar(decimal saldoInicial)
        {
            var conta = new ContaCorrente();
            conta.Creditar(saldoInicial);
            return conta;
        }

        public static ContaCorrente Criar()
        {
            var conta = new ContaCorrente();
            return conta;
        }
    }
}