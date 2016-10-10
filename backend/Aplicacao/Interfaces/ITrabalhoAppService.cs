using System;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Interfaces
{
    public interface ITrabalhoAppService
    {
        void MarcarPonto(Guid IdTrabalho);
        Trabalho GetTrabalho(string usuario);
    }
}