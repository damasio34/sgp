using System;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class PontoFactory
    {
        public static Ponto Criar(TipoEvento tipoEvento, DateTime dataHora){
            var ponto = new Ponto();
            ponto.DataHora = dataHora;
            ponto.TipoEvento = tipoEvento;

            return ponto;
        }
        public static Ponto Criar(TipoEvento tipoEvento){
            return Criar(tipoEvento, DateTime.Now);
        }
    }
}