using FluentValidation.Results;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
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
        private List<ValidationFailure> _validationResultErrors { get; set; }

        public bool ValidarUsuario()
        {
            _validationResultErrors = new ClienteValidator().Validate(this).Errors;
            return _validationResultErrors.Any();
        }
    }
}
