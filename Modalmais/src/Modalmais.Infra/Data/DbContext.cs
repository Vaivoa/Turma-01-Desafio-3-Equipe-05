using Modalmais.Business.Models;
using MongoDB.Driver;

namespace Modalmais.Infra.Data
{
    public class DbContext
    {
        const string StringConnection = "mongodb://localhost:27017";
        const string Db = "Modalmais";

        private static readonly IMongoClient _client;
        private static readonly IMongoDatabase _dataBase;


        //exemplo de uso
        //var ass = new DbContext(); _context.
        //ass.Clientes.FindAsync(); ou _context.Clientes.FindAsync();


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

    }
}
