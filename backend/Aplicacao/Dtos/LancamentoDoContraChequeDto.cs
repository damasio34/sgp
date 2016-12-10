using System;
using Damasio34.SGP.Dominio.ModuloTrabalho;

namespace Damasio34.SGP.Aplicacao.Dtos
{
    public class LancamentoDoContraChequeDto
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public TipoDeLancamento TipoDeLancamento { get; set; }
    }
}
