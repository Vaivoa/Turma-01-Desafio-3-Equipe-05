using Bogus;
using Bogus.Extensions.Brazil;
using Modalmais.Core.Models;
using Modalmais.Transacoes.API.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Test.Unitarios
{
    internal class TransacaoClassData : IEnumerable<object[]>
    {
        public static Faker bogusFaker { get; set; }
        public static ChavePix chavePix { get; set; }

        private readonly List<object[]> _data = new List<object[]>
    {
            new object[] { new Transacao(
                Core.Models.Enums.TipoChavePix.Aleatoria,
                chavePix.GerarChavePix(),
                10,"",
                new Conta("746","0001", "123456")), false},
            new object[] { new Transacao(
                Core.Models.Enums.TipoChavePix.CPF,
                bogusFaker.Person.Cpf(false),
                10,"",
                new Conta("746","0001", "123456")), false},
    };

        static TransacaoClassData()
        {
            bogusFaker = new Faker("pt_BR");
            chavePix = new ChavePix(null, Core.Models.Enums.TipoChavePix.Aleatoria);
        }

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
