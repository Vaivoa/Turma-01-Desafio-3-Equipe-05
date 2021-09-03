using Modalmais.Core.Models.Enums;
using System;
using System.Collections.Generic;

namespace Modalmais.Transacoes.API.DTOs
{
    public class ExtratoResponse
    {
        public Guid Id { get; set; }
        public string Banco => "746";
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public PeriodoReponse Periodo { get; set; }
        public List<ExtratoTransacaoResponse> Transacoes { get; set; }

    }


    public class ExtratoTransacaoResponse
    {

        public StatusTransacao StatusTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public Guid Id { get; set; }
        public DateTime DataCriacao { get; set; }
    }

    public class PeriodoReponse
    {
        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }
    }


}
