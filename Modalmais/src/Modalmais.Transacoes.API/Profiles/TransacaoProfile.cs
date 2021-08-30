using AutoMapper;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Profiles
{
    public class TransacaoProfile : Profile
    {
        public TransacaoProfile()
        {
            CreateMap<Transacao, TransacaoRequest>().ReverseMap();
            CreateMap<Transacao, TransacaoResponse>().ReverseMap();
        }
    }
}
