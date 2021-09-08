using System;

namespace Modalmais.Transacoes.API.DTOs
{
    public class ExtratoRequest
    {

        public ExtratoRequest(string agencia, string conta, PeriodoRequest periodo = null)
        {
            Agencia = agencia;
            Conta = conta;
            Periodo = periodo ?? new PeriodoRequest { DataFinal = GerarHorario(0, false), DataInicio = GerarHorario(-3) };
        }

        public string Agencia { get; set; }
        public string Conta { get; set; }
        public PeriodoRequest Periodo { get; set; }


        public void AtribuirPeriodo(DateTime dataFinal, DateTime dataInicial)
        {
            Periodo = new PeriodoRequest { DataInicio = dataInicial, DataFinal = dataFinal.AddDays(1).AddSeconds(-1) };
        }

        private DateTime GerarHorario(int dias, bool hora00 = true)
        {
            var data = DateTime.Now.ToString("yyyy-MM-dd");
            if (hora00) return DateTime.Parse(data).AddDays(dias);

            return DateTime.Parse(data).AddDays(dias).AddDays(1).AddSeconds(-1);

        }

    }



    public class PeriodoRequest
    {

        public DateTime DataInicio { get; set; }

        public DateTime DataFinal { get; set; }

    }

}
