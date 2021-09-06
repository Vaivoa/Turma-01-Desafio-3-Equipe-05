using AutoMapper;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using Modalmais.Transacoes.API.Models.ObjectValues;

namespace Modalmais.Transacoes.API.Profiles
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            CreateMap<TransacaoRequest, Transacao>()
                .ForMember(parameter => parameter.Conta, opt =>
                opt.MapFrom(s => s.ObterConta()));

            CreateMap<Transacao, TransacaoResponse>().ReverseMap();

            CreateMap<Extrato, ExtratoResponse>();

            CreateMap<ExtratoResponse, Extrato>();

            CreateMap<ExtratoRequest, Extrato>().ReverseMap();
            CreateMap<PeriodoRequest, Periodo>().ReverseMap();

            CreateMap<Transacao, ExtratoTransacaoResponse>().ReverseMap();

            CreateMap<Periodo, PeriodoReponse>().ReverseMap();
        }
    }
}
