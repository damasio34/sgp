using System;
using Damasio34.Seedwork.Exceptions;
using Damasio34.Seedwork.Extensions;
using Damasio34.SGP.Dominio.ModuloTrabalho.Resources;

namespace Damasio34.SGP.Dominio.ModuloTrabalho.Factories
{
    public static class ContraChequeFactory
    {
        public static ContraCheque Criar(Trabalho emprego, DateTime dataReferencia)
        {
            if (!dataReferencia.EhValida())
                throw new DomainException(Mensagens.DataInvalida);

            var contraCheque = new ContraCheque(emprego, dataReferencia);
            return contraCheque;
        }
    }
}