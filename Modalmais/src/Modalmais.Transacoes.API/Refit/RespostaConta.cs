using System;
using System.Collections.Generic;
using Modalmais.Core.Models.Enums;
using Refit;

namespace Modalmais.Transacoes.API.Refit
{
    public class ChavePix
    {
        public Status ativo { get; set; }
        public string chave { get; set; }
        public int tipo { get; set; }
        public DateTime dataCriacao { get; set; }
    }

    public class ContaCorrente
    {
        public string banco { get; set; }
        public string agencia { get; set; }
        public string numero { get; set; }
        public ChavePix chavePix { get; set; }
        public Status status { get; set; }
        public DateTime dataCriacao { get; set; }
    }

    public class Data
    {
        public string nome { get; set; }
        public string sobrenome { get; set; }
        public ContaCorrente contaCorrente { get; set; }
    }

    public class RespostaConta
    {
        public int statusCode { get; set; }
        public bool success { get; set; }
        public Data? data { get; set; }
        public IEnumerable<string> errors { get; set; }
    }
}