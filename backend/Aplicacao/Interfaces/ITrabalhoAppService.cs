using System;
using System.Collections.Generic;
using Damasio34.SGP.Aplicacao.Dtos;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        PontosDoDiaDto MarcarPonto(Guid idTrabalho);
        IEnumerable<PontoDto> GetPontos(Guid idTrabalho);
        PontosDoDiaDto GetPontosDoDia(Guid idTrabalho);
        Guid GetPadrao(string login);
        ContraCheque CalcularContraCheque(Guid idTrabalho);
        ContraCheque CalcularContraCheque(Guid idTrabalho, int mes);
        ConfiguracaoDto SelecionarConfiguracao(Guid idTrabalho);
        ConfiguracaoDto AtualizarConfiguracao(Guid idTrabalho, ConfiguracaoDto configuracaoDto);
    }
}