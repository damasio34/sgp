namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class ContraChequeFactory
    {
        public static ContraCheque Criar(Ciclo ciclo)
        {
            var contraCheque = new ContraCheque(ciclo);
            return contraCheque;
        }
    }
}