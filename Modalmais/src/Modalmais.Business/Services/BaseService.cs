using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Notificador;
using FluentValidation;
using FluentValidation.Results;
using Modalmais.Business.Models;
using System.Collections.Generic;

namespace Modalmais.Business.Service
{
    public abstract class BaseService { 

        protected readonly INotificador _notificador;

        protected BaseService(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected void NotificarNotificacaoMessagens(List<ValidationFailure> errors)
        {
            foreach (var error in errors)
            {
                AdicionarNotificacao(error.ErrorMessage);
            }
        }

        protected void AdicionarNotificacao(string messagem)
        {
            _notificador.AdicionarNotificacao(new Notificacao(messagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entidade
        {
            var validador = validacao.Validate(entidade);

            if (validador.IsValid) return true;

            NotificarNotificacaoMessagens(validador.Errors);
            return false;
        }
    }
}
