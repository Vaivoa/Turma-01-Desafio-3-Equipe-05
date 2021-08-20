using AutoMapper;
using Modalmais.API.DTOs;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;

namespace Modalmais.API.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteAdicionarRequest, Cliente>();
            CreateMap<ContatoAddRequest, Contato>();
            CreateMap<CelularAddRequest, Celular>();


            CreateMap<Cliente, ClienteAdicionarResponse>();
            CreateMap<Contato, ContatoAddResponse>();
            CreateMap<Celular, CelularAddResponse>();
            CreateMap<ContaCorrente, ContaCorrenteAddResponse>();


            //CreateMap<Cliente, ClienteResponse>().ReverseMap();

        }
    }
}
