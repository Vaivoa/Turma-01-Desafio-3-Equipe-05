using Modalmais.Core.Models.Enums;

namespace Modalmais.Transacoes.API.Models
{
    public class Transacao : Entidade
    {

        public Transacao(StatusTransacao statusTransacao, TipoChavePix tipo, string chave, decimal valor, string descricao)
        {
            StatusTransacao = statusTransacao;
            Tipo = tipo;
            Chave = chave;
            Valor = valor;
            Descricao = descricao == null ? "" : descricao;
        }

        public StatusTransacao StatusTransacao { get; private set; }
        public TipoChavePix Tipo { get; private set; }
        public string Chave { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }



        public bool LimiteTransferencia()
        {
            //Validar se já foi transferido 100.000.000 ou mais para uma mesma conta corrente.
            return true;
        }

    }
}
