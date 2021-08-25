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
            CreateMap<DocumentoAddRequest, Documento>();
            CreateMap<CelularAddRequest, Celular>();


            CreateMap<Cliente, ClienteAdicionarResponse>();
            CreateMap<Contato, ContatoAddResponse>();
            CreateMap<Documento, DocumentoAddResponse>();
            CreateMap<Celular, CelularAddResponse>();
            CreateMap<ContaCorrente, ContaCorrenteAddResponse>();

            CreateMap<Cliente, ClienteAdicionarDocumentoResponse>();
            CreateMap<Documento, DocumentoAddImagemResponse>();
            CreateMap<ImagemDocumento, DocumentoImagensAddImagemResponse>();

            CreateMap<Cliente, ClienteResponse>();
            CreateMap<Documento, DocumentoResponse>();
            CreateMap<ImagemDocumento, ImagemDocumentoResponse>();
            CreateMap<Contato, ContatoResponse>();
            CreateMap<Celular, CelularResponse>();
            CreateMap<ContaCorrente, ContaCorrenteResponse>();
            CreateMap<ChavePix, ChavePixResponse>();

            CreateMap<Cliente, ContaPixResponse>();

            //CreateMap<Cliente, ClienteResponse>().ReverseMap();

        }
    }
}
