namespace Damasio34.SGP.Dominio.ModuloFinanceiro
{
    public abstract class ContaBancaria : Conta
    {
        public Banco Banco { get; set; }
        public string Agencia { get; set; }
        public string DvAgencia { get; set; }
        public string Conta { get; set; }
        public string DvConta { get; set; }
    }
}