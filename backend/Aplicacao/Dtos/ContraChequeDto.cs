using System;
using System.Collections.Generic;
using System.Linq;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Dtos
{
    public class ContraChequeDto
    {
        public double ValorBruto { get; set; }        
        public DateTime? DataFinalizacao { get; set; }
        public string MesDeReferencia { get; set; }
        public double ValorLiquido { get; set; }
        public IEnumerable<LancamentoDoContraChequeDto> LancamtentosDoContraChequeDto { get; set; }

        public IEnumerable<LancamentoDoContraChequeDto> Entradas
            => LancamtentosDoContraChequeDto.Where(p => p.TipoDeLancamento.Equals(TipoDeLancamento.Entrada));    
        public IEnumerable<LancamentoDoContraChequeDto> Saidas
            => LancamtentosDoContraChequeDto.Where(p => p.TipoDeLancamento.Equals(TipoDeLancamento.Saida));
    }
}
