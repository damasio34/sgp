using System;
using System.Collections.Generic;
using Damasio34.SGP.Aplicacao.Dtos;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        PontoDto GetPonto(Guid idPonto);
        PontosDoDiaDto MarcarPonto(DateTime? dataDeReferencia);
        IEnumerable<PontoDto> GetPontos();
        PontosDoDiaDto GetPontosDoDia(DateTime dataDeReferencia);
        ContraChequeDto CalcularContraCheque();
        ContraChequeDto CalcularContraCheque(DateTime dataDeReferencia);
        ConfiguracoesDto SelecionarConfiguracoes();
        ConfiguracoesDto AlterarConfiguracoes(ConfiguracoesDto configuracoesDto);
        void DeletePonto(Guid idPonto);
        void AlterarPonto(Guid idPonto, PontoDto pontoDto);
    }
}