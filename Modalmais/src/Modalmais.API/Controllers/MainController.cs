using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Modalmais.API.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        protected readonly IMapper _mapper;

        public MainController(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static IMongoClient client = new MongoClient("mongodb://localhost:27017");
        protected IMongoDatabase context = client.GetDatabase("DesafioModal");
    }
}
