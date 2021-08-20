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

            Nome = nome;
            Sobrenome = sobrenome;
            this.CPF = CPF;
            Contato = contato;
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
            ListaDeErros = new ClienteValidator().Validate(this).Errors;
            return ListaDeErros.Any();
        }
    }
}
