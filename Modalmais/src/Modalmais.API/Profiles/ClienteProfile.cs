using AutoMapper;
using Modalmais.API.DTOs;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Core.Models;
using System.Linq;

namespace Modalmais.API.Profiles
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<ClienteAdicionarRequest, Cliente>();
            CreateMap<ContatoAddRequest, Contato>();
            CreateMap<DocumentoAddRequest, Documento>();
            CreateMap<CelularAddRequest, Celular>();


            CreateMap<Cliente, ClienteResponse>();
            CreateMap<Documento, DocumentoResponse>()
                .ForMember(parameter => parameter.Imagens, opt =>
                opt.MapFrom(s => s.Imagens.Where(o => o.Status == Core.Models.Enums.Status.Ativo)));
            CreateMap<ImagemDocumento, ImagemDocumentoResponse>();
            CreateMap<Contato, ContatoResponse>();
            CreateMap<Celular, CelularResponse>();
            CreateMap<ContaCorrente, ContaCorrenteResponse>();
            CreateMap<ChavePix, ChavePixResponse>();


            CreateMap<Cliente, ContaPixResponse>();
        }
    }
}
