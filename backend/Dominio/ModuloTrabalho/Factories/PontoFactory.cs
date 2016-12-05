using System;
using Damasio34.Seedwork.Extensions;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class PontoFactory
    {
        public static Ponto Criar(TipoDoEvento tipoDoEvento, DateTime? dataHora)
        {
            if (dataHora.IsNull()) return Criar(tipoDoEvento);

            var ponto = new Ponto
            {
                DataHora = dataHora.Value,
                TipoDoEvento = tipoDoEvento
            };
            ponto.GerarId();

            return ponto;
        }
        public static Ponto Criar(TipoDoEvento tipoDoEvento)
        {
            return Criar(tipoDoEvento, DateTime.Now);
        }
    }
}