using FluentValidation.Results;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modalmais.Business.Models
{
    public class Cliente : Entidade
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public Contato Contato { get; set; }
        public ContaCorrente ContaCorrente { get; set; }
        private List<ValidationFailure> _validationResultErrors { get; set; }
        
        //Deve retornar true para usuario valido
        public bool ValidarUsuario()
        {
            _validationResultErrors = new ClienteValidator().Validate(this).Errors;
            return !_validationResultErrors.Any();
        }
    }
}
