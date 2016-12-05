namespace Damasio34.SGP.Dominio.ModuloTrabalho
{
    public interface IImposto
    {
        TipoDoImposto TipoDoImposto { get; }
        double Valor { get; }

    }
}