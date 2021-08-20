using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Models;
using Modalmais.Infra.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{
    [Route("api/v1/clientes")]
    public class ContaCorrenteController : MainController
    {

        protected readonly IClienteRepository _clienteRepository;

        public ContaCorrenteController(IMapper mapper,
                                       DbContext context,
                                       IClienteRepository clienteRepository
                                       ) : base(mapper, context)
        {
            _clienteRepository = clienteRepository;
        }


        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteRequest clienteRequest)
        {
            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (!cliente.ValidarUsuario()) return new BadRequestObjectResult(cliente.ListaDeErros);

            await _context.Clientes.InsertOneAsync(cliente);

            return new CreatedResult(nameof(AdicionarCliente), "");

        }

        [HttpGet]
        public async Task<IActionResult> ListaClientes()
        {

            var ListaClientes = await _context.Clientes.Find(new BsonDocument()).ToListAsync();

            return new OkObjectResult(ListaClientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ClienteById(ObjectId id)
        {
            var filtro = new BsonDocument
            {
                { "_id", $"{id}"}
            };

            var Cliente = await _context.Clientes.Find(filtro).FirstOrDefaultAsync();

            if (Cliente != null) return new BadRequestObjectResult("Id não encontrado");

            return new OkObjectResult(Cliente);
        }
    }

}
