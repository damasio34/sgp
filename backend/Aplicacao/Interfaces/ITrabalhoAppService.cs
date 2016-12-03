using System;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        PontosDoDiaDto MarcarPonto(Guid idTrabalho);
        PontosDoDiaDto GetPontosDoDia(Guid idTrabalho);
        Guid GetPadrao(string login);
    }
}