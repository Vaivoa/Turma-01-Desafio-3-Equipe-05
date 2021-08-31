using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modalmais.Core.Controller;
using Modalmais.Core.Extensions;
using Modalmais.Core.Interfaces.Notificador;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Controllers
{



    [Route("")]
    public class ControllerExample : MainController
    {

        public ControllerExample(IMapper mapper,
                                  INotificador notificador
                                       ) : base(mapper, notificador)
        {

        }



        [CustomResponse(StatusCodes.Status204NoContent)]
        [CustomResponse(StatusCodes.Status400BadRequest)]
        [CustomResponse(StatusCodes.Status404NotFound)]
        [HttpPost("")]
        public async Task<IActionResult> AdicionarCliente()
        {
            if (ModelState.IsValid) return ResponseModelErro(ModelState);



            return ResponseNoContent();
        }


    }
}