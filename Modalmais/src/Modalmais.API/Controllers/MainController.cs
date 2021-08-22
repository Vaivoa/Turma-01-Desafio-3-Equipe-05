using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Modalmais.API.Controllers.Shared;
using Modalmais.API.Extensions;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Notificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Modalmais.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly INotificador _notificador;


        public MainController(IMapper mapper,
                              INotificador notificador)
        {
            _mapper = mapper;
            _notificador = notificador;
        }
        protected bool NotificadorContemErros()
        {
            return _notificador.TemNotificacao();
        }

        protected IEnumerable<string> ObterErrosNotificador()
        {
            return _notificador.ListaNoticacoes().Select(n => n.Mensagem);
        }

        protected IActionResult ResponseModelErro(ModelStateDictionary modelState)
        {
            AdicionarNotificacaoErro(modelState);
            return ResponseBadRequest();
        }

        protected IActionResult ResponseEntidadeErro(List<ValidationFailure> erro)
        {
            AdicionarNotificacaoErro(erro);
            return ResponseBadRequest();
        }

        protected void AdicionarNotificacaoErro(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AdicionarNotificacaoErro(errorMsg);
            }
        }

        protected new JsonResult Response(HttpStatusCode statusCode, object data, string errorMessage)
        {
            CustomResult result = null;

            if (string.IsNullOrWhiteSpace(errorMessage) && !NotificadorContemErros())
            {
                var success = statusCode.IsSuccess();

                if (data != null)
                    result = new CustomResult(statusCode, success, data);
                else
                    result = new CustomResult(statusCode, success);
            }
            else
            {
                var errors = new List<string>();

                if (!string.IsNullOrWhiteSpace(errorMessage))
                    errors.Add(errorMessage);

                if (NotificadorContemErros())
                    errors.AddRange(ObterErrosNotificador());

                result = new CustomResult(statusCode, false, errors);
            }
            return new JsonResult(result) { StatusCode = (int)result.StatusCode };
        }

        protected void AdicionarNotificacaoErro(string mensagem)
        {
            _notificador.AdicionarNotificacao(new Notificacao(mensagem));
        }

        protected void AdicionarNotificacaoErro(ValidationFailure erro)
        {
            AdicionarNotificacaoErro(erro.ErrorMessage);
        }

        protected void AdicionarNotificacaoErro(List<ValidationFailure> lista)
        {
            foreach (var erro in lista) AdicionarNotificacaoErro(erro.ErrorMessage);
        }


        protected IActionResult ResponseOk(object result) =>
            Response(HttpStatusCode.OK, result);

        protected IActionResult ResponseOk() =>
            Response(HttpStatusCode.OK);

        protected IActionResult ResponseCreated() =>
            Response(HttpStatusCode.Created);

        protected IActionResult ResponseCreated(object data) =>
            Response(HttpStatusCode.Created, data);

        protected IActionResult ResponseNoContent() =>
            Response(HttpStatusCode.NoContent);

        protected IActionResult ResponseNotModified() =>
            Response(HttpStatusCode.NotModified);

        protected IActionResult ResponseBadRequest(string errorMessage) =>
            Response(HttpStatusCode.BadRequest, errorMessage: errorMessage);

        protected IActionResult ResponseBadRequest() =>
            Response(HttpStatusCode.BadRequest, errorMessage: "A requisição é inválida");

        protected IActionResult ResponseNotFound(string errorMessage) =>
            Response(HttpStatusCode.NotFound, errorMessage: errorMessage);

        protected IActionResult ResponseNotFound() =>
            Response(HttpStatusCode.NotFound, errorMessage: "O recurso não foi encontrado");

        protected IActionResult ResponseUnauthorized(string errorMessage) =>
            Response(HttpStatusCode.Unauthorized, errorMessage: errorMessage);

        protected IActionResult ResponseUnauthorized() =>
            Response(HttpStatusCode.Unauthorized, errorMessage: "Permissão negada");

        protected IActionResult ResponseForbidden(string errorMessage) =>
            Response(HttpStatusCode.Forbidden, errorMessage: errorMessage);

        protected IActionResult ResponseForbidden() =>
            Response(HttpStatusCode.Forbidden, errorMessage: "Permissão negada");

        protected IActionResult ResponseInternalServerError() =>
            Response(HttpStatusCode.InternalServerError);

        protected IActionResult ResponseInternalServerError(string errorMessage) =>
            Response(HttpStatusCode.InternalServerError, errorMessage: errorMessage);

        protected IActionResult ResponseInternalServerError(Exception exception) =>
            Response(HttpStatusCode.InternalServerError, errorMessage: exception.Message);

        protected new JsonResult Response(HttpStatusCode statusCode, object result) =>
            Response(statusCode, result, null);

        protected new JsonResult Response(HttpStatusCode statusCode, string errorMessage) =>
            Response(statusCode, null, errorMessage);

        protected new JsonResult Response(HttpStatusCode statusCode) =>
            Response(statusCode, null, null);


    }
}
