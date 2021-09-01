using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.Models;

namespace Modalmais.Transacoes.API.DTOs
{
    public class TransacaoRequest
    {

        public TipoChavePix Tipo { get; set; }
        public string Chave { get; set; }
        public decimal Valor { get; set; }
        public string? Descricao { get; set; }
        private Conta Conta { get; set; }

        public void AtribuirConta(string numero)
        {
            Conta = new Conta("746", "0001", numero);
        }

        public Conta ObterConta()
        {
            return Conta;

        }

        public string ObterNumeroConta() => Conta.Numero;


    }
}
