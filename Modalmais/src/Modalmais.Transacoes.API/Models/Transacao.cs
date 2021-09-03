using FluentValidation.Results;
using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.Models.Validations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Modalmais.Transacoes.API.Models
{
    public class Transacao : Entidade
    {

        public Transacao(TipoChavePix tipo, string chave, decimal valor, string descricao, Conta conta)
        {
            StatusTransacao = StatusTransacao.NaoConcluido;
            Tipo = tipo;
            Chave = chave;
            Valor = valor;
            Descricao = descricao ?? "";
            Conta = conta;
            ListaDeErros = new List<ValidationFailure>();

        }

        private Transacao(TipoChavePix tipo, string chave, decimal valor, string descricao)
        {
            StatusTransacao = StatusTransacao.NaoConcluido;
            Tipo = tipo;
            Chave = chave;
            Valor = valor;
            Descricao = descricao ?? "";
            ListaDeErros = new List<ValidationFailure>();
        }        

        public Transacao() { }

        public StatusTransacao StatusTransacao { get;  set; }
        public TipoChavePix Tipo { get;  set; }
        public string Chave { get;  set; }
        [Range(0.01, 5000.00)]
        public decimal Valor { get;  set; }
        public string Descricao { get;  set; }
        public Conta Conta { get;  set; }


        public void ConcluirTransacao() { StatusTransacao = StatusTransacao.Concluido; }
        public void CancelarTransacao() { StatusTransacao = StatusTransacao.Cancelado; }

        public bool LimiteAtingido(decimal valor) { return valor > 100000; }


        public List<ValidationFailure> ListaDeErros { get; private set; }

        public bool EstaInvalido()
        {
            ListaDeErros.Clear();

            ListaDeErros = new TransacaoValidator().Validate(this).Errors;

            return ListaDeErros.Any();
        }

    }
}
