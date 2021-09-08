using Modalmais.Core.Models;
using Modalmais.Core.Models.Enums;
using System;

namespace Modalmais.Business.Models
{
    public class ContaCorrente
    {
        public string Banco { get; private set; }
        public string Agencia { get; private set; }
        public string Numero { get; private set; }
        public ChavePix? ChavePix { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public DateTime DataMudancaStatus { get; private set; }
        public Status Status { get; private set; }

        public ContaCorrente()
        {
            Banco = "746";
            Agencia = "0001";
            Numero = GerarNumeroConta();
            DataCriacao = DateTime.Now;
            Status = Status.Inativo;
            DataMudancaStatus = DateTime.Now;
        }

        public string GerarNumeroConta()
        {

            var numeroConta = "";
            var random = new Random();

            for (int i = 0; i < 16; i++)
            {
                numeroConta += random.Next(0, 10).ToString();
            }

            return numeroConta;
        }

        public void AtivarConta()
        {
            Status = Status.Ativo;
            DataMudancaStatus = DateTime.Now;
        }

        public void AdicionarChavePix(ChavePix chavePix)
        {
            ChavePix = chavePix;
        }

        public void DesativarConta()
        {
            Status = Status.Desativado;
            DataMudancaStatus = DateTime.Now;
        }

    }
}
