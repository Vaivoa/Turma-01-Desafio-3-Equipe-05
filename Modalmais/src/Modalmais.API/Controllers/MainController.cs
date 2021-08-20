using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Notificador;
using Modalmais.Infra.Data;
using System.Linq;

namespace Modalmais.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly DbContext _context;
        protected readonly INotificador _notificador;


        public MainController(IMapper mapper,
                              DbContext context, INotificador notificador)
        {
            _mapper = mapper;
            _context = context;
            _notificador = notificador;
        }
        protected bool ValidarEntidadeListaContemErros()
        {
            //Retorna True se tiver erros lançados na camada de bussines ou data
            return _notificador.TemNotificacao();
        }

        protected ActionResult NotificarErrorsEmLista(object result = null)
        {
            //Retorna lista de _notificacoes da classe Notificador
            return BadRequest(new
            {
                success = false,
                errors = _notificador.ListaNoticacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult NotificarErrorsEmLista(ModelStateDictionary modelState)
        {
            //Metodo para validar e notificar errors da modelState
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return NotificarErrorsEmLista();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            //metodo para adicionar errors da modelState na lista _notificacoes 
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                AdicionarNotificacaoErro(errorMsg);
            }
        }

        protected void AdicionarNotificacaoErro(string mensagem)
        {
            //Metodo para adicionar mensagem personalizadas de erro
            _notificador.AdicionarNotificacao(new Notificacao(mensagem));
        }
    }
}
