using Modalmais.Core.Models.Enums;

namespace Modalmais.Transacoes.API.Models
{
    public class Transacao : Entidade
    {

        public Transacao(TipoChavePix tipo, string chave, decimal valor, string descricao)
        {
            StatusTransacao = StatusTransacao.NaoConcluido;
            Tipo = tipo;
            Chave = chave;
            Valor = valor;
            Descricao = descricao ?? "";
        }

        public StatusTransacao StatusTransacao { get; private set; }
        public TipoChavePix Tipo { get; private set; }
        public string Chave { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }

        public bool LimiteAtingido(decimal valor){ return valor > 100000; }

    }
}
