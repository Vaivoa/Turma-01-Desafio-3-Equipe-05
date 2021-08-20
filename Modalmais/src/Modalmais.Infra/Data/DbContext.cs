using Modalmais.Business.Models;
using MongoDB.Driver;
using System;

namespace Modalmais.Infra.Data
{
    public class DbContext : IDisposable
    {
        const string StringConnection = "mongodb://localhost:27017";
        const string Db = "Modalmais";

        private static readonly IMongoClient _client;
        public static IMongoDatabase _dataBase;

        public IMongoDatabase Database => _client.GetDatabase(Db);


        //exemplo de uso
        //var foo = new DbContext(); _context.
        //foo.Clientes.FindAsync(); ou _context.Clientes.FindAsync();

        static DbContext()
        {
            _client = new MongoClient(StringConnection);
            _dataBase = _client.GetDatabase(Db);
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
