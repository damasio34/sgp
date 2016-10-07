using System;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class PontoFactory
    {
        public static Ponto Criar(TipoDoEvento tipoDoEvento, DateTime dataHora){
            var ponto = new Ponto();
            ponto.DataHora = dataHora;
            ponto.TipoDoEvento = tipoDoEvento;

            return ponto;
        }
        public static Ponto Criar(TipoDoEvento tipoDoEvento){
            return Criar(tipoDoEvento, DateTime.Now);
        }
    }
}