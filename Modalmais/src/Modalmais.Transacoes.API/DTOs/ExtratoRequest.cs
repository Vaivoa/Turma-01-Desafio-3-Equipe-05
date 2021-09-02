using System;

namespace Modalmais.Transacoes.API.DTOs
{
    public class ExtratoRequest
    {

        public ExtratoRequest(string agencia, string conta, PeriodoRequest periodo)
        {
            Agencia = agencia;
            Conta = conta;
            Periodo = periodo ?? new PeriodoRequest { DataFinal = DateTime.Now, DataInicio = DateTime.Now.AddDays(-3) };
        }

        public string Agencia { get; set; }
        public string Conta { get; set; }
        public PeriodoRequest Periodo { get; set; }
    }



    public class PeriodoRequest
    {

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }

    }

}
