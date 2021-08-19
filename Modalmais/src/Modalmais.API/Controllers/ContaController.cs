using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using Modalmais.Business.Models;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{
    [Route("Cliente")]
    public class ContaCorrenteController : MainController
    {
        public ContaCorrenteController(IMapper mapper) : base(mapper) { }

        [HttpPost]
        public async Task<IActionResult> Cliente(ClienteRequest clienteRequest)
        {
            IMongoCollection<Cliente> clientes = context.GetCollection<Cliente>("Clientes");

            var cliente = _mapper.Map<Cliente>(clienteRequest);

            if (!cliente.ValidarUsuario()) return new BadRequestObjectResult(cliente);

            await clientes.InsertOneAsync(cliente);

            return new OkObjectResult("");

        }
    }


}
