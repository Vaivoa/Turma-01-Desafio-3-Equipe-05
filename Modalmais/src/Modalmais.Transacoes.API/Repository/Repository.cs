﻿using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Modalmais.Transacoes.API.Data;
using Modalmais.Transacoes.API.DTOs;
using Modalmais.Transacoes.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Modalmais.Transacoes.API.Repository
{
    public abstract class Repository<TEntity> : IDisposable where TEntity : Entidade
    {

        protected readonly ApiDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        private readonly IMapper _mapper;

        protected Repository(ApiDbContext db, IMapper mapper)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
            _mapper = mapper;
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();
        }

        public async Task<bool> Existe(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTrackingWithIdentityResolution().Where(predicate).AnyAsync();
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<Extrato> ObterPorContaDapper(ExtratoRequest extratoRequest)
        {
            var conn = Db.Database.GetDbConnection();

            string dataInicio = extratoRequest.Periodo.DataInicio.ToString("yyyy/MM/dd");
            string dataFim = extratoRequest.Periodo.DataFinal.ToString("yyyy/MM/dd");

            var updateSQL =
                string.Format(@"SELECT * FROM modalmais.""Transacoes"" WHERE ""Conta_Agencia"" = '{0}' AND ""Conta_Numero"" = '{1}' AND ""DataCriacao"" >= '{2}' AND ""DataCriacao"" <= '{3} 23:59:59'",
                    extratoRequest.Agencia, extratoRequest.Conta, dataInicio, dataFim);

            var data = await conn.QueryAsync<dynamic>(updateSQL);

            var extrato = _mapper.Map<Extrato>(extratoRequest);
            extrato.AtirbuirTrancacoes(_mapper.Map<List<Transacao>>(data));
            extrato.ObterTotalValorMovimentadoDurantePeriodo();
            return extrato;
        }

        public virtual async Task<bool> TransacoesDisponiveis(string conta)
        {
            var conn = Db.Database.GetDbConnection();
            var updateSQL = string.Format(@"SELECT * FROM modalmais.""Transacoes"" WHERE ""Conta_Numero"" = '{0}'", conta);
            var data = await conn.QueryAsync<dynamic>(updateSQL);
            return data.Any();
        }

        public virtual async Task<bool> Salvar()
        {
            return Db.SaveChangesAsync().Result > 0;
        }

        public virtual void Add(TEntity entidade)
        {
            Db.Add(entidade);
        }


        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}