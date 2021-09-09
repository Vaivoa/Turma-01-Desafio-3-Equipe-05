using Modalmais.Business.Interfaces.Repository;
using Modalmais.Business.Models;
using Modalmais.Infra.Data;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Modalmais.Infra.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entidade
    {
        protected readonly MongoDbContext _context;
        protected readonly IMongoCollection<TEntity> DbSet;

        protected Repository(MongoDbContext context)
        {
            _context = context;
            DbSet = _context.Database.GetCollection<TEntity>(typeof(TEntity).Name + "s");
        }


        public virtual async Task Adicionar(TEntity obj)
        {
            await DbSet.InsertOneAsync(obj);
        }

        public virtual async Task<TEntity> ObterPorId(string id)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq("Id", id));
            return data.FirstOrDefault();
        }

        public virtual async Task<TEntity> Buscar(string campo, string comparar)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(campo, comparar));
            return data.FirstOrDefault();
        }


        public virtual async Task<TEntity> BuscarComFiltro(FilterDefinition<TEntity> field)
        {
            var data = await DbSet.FindAsync(field);
            return data.FirstOrDefault();
        }


        public virtual async Task<bool> ChecarEntidadeExistente(string campo, string comparar)
        {
            var data = await DbSet.FindAsync(Builders<TEntity>.Filter.Eq(campo, comparar));
            return data.Any();
        }

        public virtual async Task<bool> ChecarEntidadeExistente(FilterDefinition<TEntity> filter)
        {
            var data = await DbSet.FindAsync(filter);
            return data.Any();
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            var all = await DbSet.FindAsync(Builders<TEntity>.Filter.Empty);
            return all.ToList();
        }

        public virtual async Task Update(TEntity obj)
        {

            await DbSet.ReplaceOneAsync(Builders<TEntity>.Filter.Eq(o => o.Id, obj.Id), obj);

        }

        public virtual async Task Remove(string id)
        {

            await DbSet.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", id));

        }

        public void Dispose()
        {
            _context?.Dispose();
        }

    }
}
