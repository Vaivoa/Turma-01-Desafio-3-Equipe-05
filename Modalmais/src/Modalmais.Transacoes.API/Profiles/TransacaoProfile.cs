using AutoMapper;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;

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
        }
    }
}
