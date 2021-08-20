using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Modalmais.Infra.Data;

namespace Modalmais.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly DbContext _context;


        public MainController(IMapper mapper,
                              DbContext context)
        {
            _mapper = mapper;
            _context = context;

        }

    }
}
