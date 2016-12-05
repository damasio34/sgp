using System;
using System.Collections.Generic;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        PontosDoDiaDto MarcarPonto(Guid idTrabalho);
        IEnumerable<Ponto> GetPontos(Guid idTrabalho);
        PontosDoDiaDto GetPontosDoDia(Guid idTrabalho);
        Guid GetPadrao(string login);        
    }
}