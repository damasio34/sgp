using System;
using System.Collections.Generic;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        PontosDoDiaDto MarcarPonto();
        IEnumerable<PontoDto> GetPontos();
        PontosDoDiaDto GetPontosDoDia();
        ContraCheque CalcularContraCheque();
        ContraCheque CalcularContraCheque(DateTime dataDeReferencia);
        ConfiguracaoDto SelecionarConfiguracao();
        ConfiguracaoDto AtualizarConfiguracao(ConfiguracaoDto configuracaoDto);
        void DeletePonto(Guid idPonto);
    }
}