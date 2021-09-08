using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.Models;
using System;

namespace Modalmais.Transacoes.API.DTOs
{

    public class TransacaoResponse
    {

        public TransacaoResponse(StatusTransacao statusTransacao, TipoChavePix tipo, string chave, decimal valor, string descricao, Guid id, DateTime dataCriacao, Conta conta)
        {
            StatusTransacao = statusTransacao;
            Tipo = tipo;
            Chave = chave;
            Valor = valor;
            Descricao = descricao;
            Id = id;
            DataCriacao = dataCriacao;
            Conta = conta;
        }

        public StatusTransacao StatusTransacao { get; private set; }
        public TipoChavePix Tipo { get; private set; }
        public string Chave { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }
        public Guid Id { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Conta Conta { get; private set; }

    }
}
