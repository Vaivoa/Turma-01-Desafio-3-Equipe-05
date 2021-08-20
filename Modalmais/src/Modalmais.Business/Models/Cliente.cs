using FluentValidation.Results;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using System.Linq;

namespace Modalmais.Business.Models
{
    public class Cliente : Entidade
    {

        public Cliente(string nome, string sobrenome, string CPF, Contato contato)
        {
            ListaDeErros = new List<ValidationFailure>();
            Nome = nome;
            Sobrenome = sobrenome;
            this.CPF = CPF;
            Contato = contato;
            ContaCorrente = new ContaCorrente();
            ValidarUsuario();
        }


        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string CPF { get; private set; }
        public Contato Contato { get; private set; }
        public ContaCorrente ContaCorrente { get; private set; }



        [BsonIgnore]
        public List<ValidationFailure> ListaDeErros { get; private set; }



        // Retorna True se tiverem erros
        public bool ValidarUsuario()
        {
            ListaDeErros.Clear();

            ListaDeErros = new ClienteValidator().Validate(this).Errors;

            return ListaDeErros.Any();
        }


        //public string GerarNumeroConta()
        //{

        //    var numeroConta = "";
        //    var random = new Random();

        //    for (int i = 0; i < 16; i++)
        //    {
        //        numeroConta += random.Next(0, 10).ToString();
        //    }

        //    return numeroConta;
        //}

    }
}
