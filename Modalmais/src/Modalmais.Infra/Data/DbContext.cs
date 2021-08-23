using Modalmais.Business.Models;
using MongoDB.Driver;
using System;

namespace Modalmais.Infra.Data
{
    public class DbContext : IDisposable
    {
        const string StringConnection = "mongodb://localhost:27017";
        const string Db = "Modalmais";

        static DbContext instance;
        private static readonly IMongoClient _client;
        private readonly IMongoClient _clientTeste;
        public static IMongoDatabase _dataBase;
        public readonly IMongoDatabase _dataBaseTeste;

        public IMongoDatabase Database => _client.GetDatabase(Db);


        //exemplo de uso
        //var foo = new DbContext(); _context.
        //foo.Clientes.FindAsync(); ou _context.Clientes.FindAsync();

        static DbContext()
        {
            _client = new MongoClient(StringConnection);
            _dataBase = _client.GetDatabase(Db);
        }
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
            //GC.SuppressFinalize(this);
        }
    }
}
