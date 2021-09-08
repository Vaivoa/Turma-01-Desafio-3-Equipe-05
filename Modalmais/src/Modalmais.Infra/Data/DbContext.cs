using Modalmais.Business.Models;
using MongoDB.Driver;
using System;

namespace Modalmais.Infra.Data
{
    public class DbContext : IDisposable
    {

        static DbContext instance;
        private static readonly IMongoClient _client;
        private readonly IMongoClient _clientTeste;
        private static IMongoDatabase _dataBase;

        public IMongoDatabase Database => _dataBase;

        public DbContext(string connectionString = null, string db = null)
        {
            if (connectionString == null || db == null) return;
            _clientTeste = new MongoClient(connectionString);
            _dataBase = _clientTeste.GetDatabase(db);
        }
        public IMongoClient Client
        {
            get
            {
                return _client;
            }
        }

        public IMongoCollection<Cliente> Clientes
        {
            get
            {
                return _dataBase.GetCollection<Cliente>("Clientes");
            }
        }

        public void Dispose()
        {

        }
    }
}
