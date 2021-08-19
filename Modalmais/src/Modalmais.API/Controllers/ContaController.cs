using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modalmais.API.DTOs;
using System.Threading.Tasks;

namespace Modalmais.API.Controllers
{
    [Route("Cliente")]
    public class ContaCorrenteController : MainController
    {
        public ContaCorrenteController(IMapper mapper) : base(mapper) { }

    }

    [HttpPost]
    public async Task<IActionResult> Cliente(ClienteRequest clienteRequest)
    {
        
    }
}
