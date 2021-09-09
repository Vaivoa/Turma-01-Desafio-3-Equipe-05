using Bogus;
using Bogus.Extensions.Brazil;
using Modalmais.Business.Models;
using Modalmais.Core.Models;
using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.Models;
using System.Collections;
using System.Collections.Generic;

namespace Modalmais.Test.Unitarios
{
    internal class TransacaoClassData : IEnumerable<object[]>
    {
        public static Faker bogusFaker { get; set; }
        public static ChavePix chavePix { get; set; }

        public static ContaCorrente contaCorrente { get; set; }

        private readonly List<object[]> _data = new ()
    {
            new object[] { new Transacao(
                TipoChavePix.Aleatoria,
                chavePix.GerarChavePix(),
                10,"",
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), false},
            new object[] { new Transacao(
                TipoChavePix.CPF,
                bogusFaker.Person.Cpf(false),
                10,"",
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), false},
            new object[] { new Transacao(
                TipoChavePix.Telefone,
                $"{((int)bogusFaker.PickRandom<DDDBrasil>())}{bogusFaker.Random.Number(900000000, 999999999)}",
                10,"",
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), false},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(),
                10,"",
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), false},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(),
                10,"",
                new Conta("746","", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(),
                10,"",
                new Conta("746","", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(),
                10,"",
                new Conta("746","0001", "123456")), true},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(),
                10,bogusFaker.Random.String2(51),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(),
                5001,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                (TipoChavePix)10,
                bogusFaker.Internet.Email(),
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Email,
                bogusFaker.Internet.Email(bogusFaker.Random.String2(50)),
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Telefone,
                $"{((int)bogusFaker.PickRandom<DDDBrasil>())}9{bogusFaker.Random.Number(900000000, 999999999)}",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Telefone,
                $"{((int)bogusFaker.PickRandom<DDDBrasil>())}{bogusFaker.Random.Number(90000000, 99999999)}",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Telefone,
                $"0{((int)bogusFaker.PickRandom<DDDBrasil>())}{bogusFaker.Random.Number(90000000, 99999999)}",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.CPF,
                bogusFaker.Random.Digits(12).ToString(),
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.CPF,
                bogusFaker.Random.Digits(9).ToString(),
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Aleatoria,
                bogusFaker.Random.String2(33).ToString(),
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Aleatoria,
                "",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Email,
                "",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.Telefone,
                "",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
            new object[] { new Transacao(
                TipoChavePix.CPF,
                "",
                5000,bogusFaker.Random.String2(50),
                new Conta("746","0001", contaCorrente.GerarNumeroConta())), true},
    };

        static TransacaoClassData()
        {
            bogusFaker = new Faker("pt_BR");
            chavePix = new ChavePix(null, Core.Models.Enums.TipoChavePix.Aleatoria);
            contaCorrente = new ContaCorrente();
        }

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
