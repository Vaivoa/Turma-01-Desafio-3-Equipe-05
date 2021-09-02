using FluentValidation.Results;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
using Modalmais.Core.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Modalmais.Business.Models
{
    public class Cliente : Entidade
    {

        public Cliente(string nome, string sobrenome, Contato contato, Documento documento)
        {
            ListaDeErros = new List<ValidationFailure>();
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Contato = contato;
            ContaCorrente = new ContaCorrente();

        }


        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public Documento Documento { get; private set; }
        public Contato Contato { get; private set; }
        public ContaCorrente ContaCorrente { get; private set; }
        public DateTime DataAlteracao { get; private set; }

        public void ValidarDataAlteracao()
        {
            DataAlteracao = DateTime.Now;
        }

        [BsonIgnore]
        public List<ValidationFailure> ListaDeErros { get; private set; }

        public void AlterarCliente(string nome, string sobrenome, string email, DDDBrasil ddd, string numero)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Contato.SetarEmail(email);
            Contato.Celular.SetarCelular(ddd, numero);
        }
        // Retorna True se tiverem erros
        public bool EstaInvalido()
        {
            ListaDeErros.Clear();

            ListaDeErros = new ClienteValidator().Validate(this).Errors;

            return ListaDeErros.Any();
        }

        public override string ToString()
        {
            return $"Id: ,Nome: {Nome},SobreNome: {Sobrenome}, Telefone: {((int)Contato.Celular.DDD)}{Contato.Celular.Numero}, Email: {Contato.Email}";
        }

    }
}
