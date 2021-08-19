using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Modalmais.API.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {
        protected readonly IMapper _mapper;

        public MainController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
