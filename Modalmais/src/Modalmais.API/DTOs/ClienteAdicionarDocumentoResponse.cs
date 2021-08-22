using Modalmais.Business.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Modalmais.API.DTOs
{
    public class ClienteAdicionarDocumentoResponse
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DocumentoAddImagemResponse Documento { get; set; }
        public ContatoAddResponse Contato { get; set; }
        public ContaCorrenteAddResponse ContaCorrente { get; set; }

    }

    public class DocumentoAddImagemResponse
    {
        public string CPF { get; set; }
        public Status Status { get; set; }
        public string UrlImagem { get; set; }
        public string NomeImagem { get; set; }

    }

}