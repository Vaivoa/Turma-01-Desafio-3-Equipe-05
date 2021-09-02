using Modalmais.Transacoes.API.Models.ObjectValues;

namespace Modalmais.Transacoes.API.Models
{
    public class Extrato : Entidade
    {
        public Extrato(string agencia, string conta, Periodo periodo)
        {
            Agencia = agencia;
            Conta = conta;
            Periodo = periodo;
        }

        protected Extrato()
        {
            //para realizar as migrations
        }

        public string Agencia { get; set; }
        public string Conta { get; set; }
        public Periodo Periodo { get; set; }

    }
}
