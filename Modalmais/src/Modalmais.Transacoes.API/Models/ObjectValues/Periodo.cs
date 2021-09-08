using System;

namespace Modalmais.Transacoes.API.Models.ObjectValues
{
    public class Periodo
    {

        public Periodo(DateTime dataInicio, DateTime dataFinal)
        {
            DataInicio = dataInicio;
            DataFinal = dataFinal;
        }

        public DateTime DataInicio { get; private set; }

        public DateTime DataFinal { get; private set; }

    }
}
