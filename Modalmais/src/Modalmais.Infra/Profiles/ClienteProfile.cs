using AutoMapper;
using Modalmais.API.DTOs;
using Modalmais.Business.Models;

namespace Modalmais.Infra.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteRequest>().ReverseMap();
            CreateMap<Cliente, ClienteResponse>().ReverseMap();
        }
    }
}
