using System;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        void MarcarPonto(Guid idTrabalho);
        PontosDoDiaDto GetPontosDoDia(string login);
    }
}