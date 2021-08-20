using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.Business.Interfaces.Notificador;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Models;
using Modalmais.Infra.Data;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{
    [Route("api/v1/clientes")]
    public class ContaCorrenteController : MainController
    {

        protected readonly IClienteRepository _clienteRepository;

        public ContaCorrenteController(IMapper mapper,
                                       DbContext context,
                                       INotificador notificador,
                                       IClienteRepository clienteRepository
                                       ) : base(mapper, context, notificador)
        {
            _clienteRepository = clienteRepository;
        }


        [HttpPost]
        public async Task<IActionResult> Cliente(ClienteRequest clienteRequest)
        {
            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (!cliente.ValidarUsuario()) return new BadRequestObjectResult(cliente);

            await _context.Clientes.InsertOneAsync(cliente);

            return new OkObjectResult("");

        }

        [HttpGet("")]
        public async Task<IActionResult> ObterTodosClientes()
        {

            var clientes = await _clienteRepository.ObterTodos();

            //var clientes = await _context.Clientes.FindAsync<Cliente>(new BsonDocument());

            return new OkObjectResult(clientes);

        }
    }


}
