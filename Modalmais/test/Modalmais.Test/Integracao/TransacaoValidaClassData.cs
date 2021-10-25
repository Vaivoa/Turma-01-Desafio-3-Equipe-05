using Bogus;
using Bogus.Extensions.Brazil;
using Modalmais.Business.Models;
using Modalmais.Core.Models;
using Modalmais.Core.Models.Enums;
using Modalmais.Transacoes.API.DTOs;
using System.Collections;
using System.Collections.Generic;

namespace Modalmais.Test.Integracao
{
    public class TransacaoValidaClassData : IEnumerable<object[]>
    {
        public static Faker bogusFaker { get; set; }
        public static ChavePix chavePix { get; set; }

        public static ContaCorrente contaCorrente { get; set; }

        private readonly List<object[]> _data = new()
        {
            new object[] { new TransacaoRequest(TipoChavePix.Aleatoria, chavePix.GerarChavePix(), 5000, ""), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, bogusFaker.Person.Cpf(false), 5000, ""), false },
            new object[] { new TransacaoRequest(TipoChavePix.Telefone, $"{(int)bogusFaker.PickRandom<DDDBrasil>()}" +
                $"{bogusFaker.Random.Number(900000000, 999999999)}", 5000,""), false},
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(), 5000, ""), false },
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(), 5000, ""), false },
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(), 5000, ""), false },
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(), 5000, ""), false },
            //
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(), 5000, bogusFaker.Random.String2(31)), true },
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(), 5001, bogusFaker.Random.String2(30)), true },
            //new object[] { new TransacaoRequest((TipoChavePix)10,bogusFaker.Internet.Email(),5000,bogusFaker.Random.String2(30)), true},
            //
            new object[] { new TransacaoRequest(TipoChavePix.Email, bogusFaker.Internet.Email(bogusFaker.Random.String2(30)), 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Telefone, $"{(int)bogusFaker.PickRandom<DDDBrasil>()}{bogusFaker.Random.Number(900000000, 999999999)}", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Telefone, $"{(int)bogusFaker.PickRandom<DDDBrasil>()}{bogusFaker.Random.Number(90000000, 99999999)}", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Telefone, $"{(int)bogusFaker.PickRandom<DDDBrasil>()}{bogusFaker.Random.Number(90000000, 99999999)}", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, bogusFaker.Random.Digits(12).ToString(), 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, bogusFaker.Random.Digits(9).ToString(), 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Aleatoria, bogusFaker.Random.String2(33).ToString(), 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Aleatoria, "", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Email, "", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.Telefone, "", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, "", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, "", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, "", 5000, bogusFaker.Random.String2(30)), false },
            new object[] { new TransacaoRequest(TipoChavePix.CPF, "", 5000, bogusFaker.Random.String2(30)), true },
        };

        static TransacaoValidaClassData()
        {
            bogusFaker = new Faker("pt_BR");
            chavePix = new ChavePix(null, TipoChavePix.Aleatoria);
            contaCorrente = new ContaCorrente();
        }

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
