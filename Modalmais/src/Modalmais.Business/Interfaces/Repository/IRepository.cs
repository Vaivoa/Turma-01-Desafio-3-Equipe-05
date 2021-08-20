﻿using Modalmais.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modalmais.Business.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entidade
    {

        Task Adicionar(TEntity obj);


        Task<TEntity> ObterPorId(string id);

        Task<TEntity> Buscar(string campo, string comparar);


        Task<IEnumerable<TEntity>> ObterTodos();


        Task Update(TEntity obj);


        Task Remove(string id);

    }
}