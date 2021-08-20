using Modalmais.Business.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Modalmais.API.DTOs
{
    public class ClienteAdicionarResponse
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string CPF { get; set; }
        public ContatoAddResponse Contato { get; set; }
        public ContaCorrenteAddResponse ContaCorrente { get; set; }

    }

    public class ContatoAddResponse
    {
        public CelularAddResponse Celular { get; set; }
        public string Email { get; set; }

    }

    public class CelularAddResponse
    {
        public DDDBrasil DDD { get; set; }
        public string Numero { get; set; }

    }

    public class ContaCorrenteAddResponse
    {
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Numero { get; set; }

    }

}