using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.Business.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{
    [Route("Cliente")]
    public class ContaCorrenteController : MainController
    {
        public ContaCorrenteController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        public async Task<IActionResult> AdicionarCliente(ClienteRequest clienteRequest)
        {
            IMongoCollection<Cliente> clientes = context.GetCollection<Cliente>("Clientes");

            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (!cliente.ValidarUsuario()) return new BadRequestObjectResult(cliente.ListaDeErros);

            await clientes.InsertOneAsync(cliente);

            return new CreatedResult(nameof(AdicionarCliente), "");

        }

        [HttpGet]
        public async Task<IActionResult> ListaClientes()
        {
            IMongoCollection<Cliente> clientes = context.GetCollection<Cliente>("Clientes");

            var ListaClientes = await clientes.Find(new BsonDocument()).ToListAsync();

            return new OkObjectResult(ListaClientes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ClienteById(ObjectId id)
        {
            IMongoCollection<Cliente> clientes = context.GetCollection<Cliente>("Clientes");

            var filtro = new BsonDocument
            {
                { "_id", $"{id}"}
            };

            var Cliente = await clientes.Find(filtro).FirstOrDefaultAsync();

            if (Cliente != null) return new BadRequestObjectResult("Id não encontrado");

            return new OkObjectResult(Cliente);
        }
    }

}
