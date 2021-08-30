using Modalmais.Core.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace Modalmais.API.DTOs
{
    public class ClienteAdicionarDocumentoResponse
    {
        public ClienteAdicionarDocumentoResponse()
        {
        }

        public ClienteAdicionarDocumentoResponse(string id, string nome, string sobrenome, DocumentoAddImagemResponse documento, ContatoAddResponse contato, ContaCorrenteAddResponse contaCorrente)
        {
            Id = id;
            Nome = nome;
            Sobrenome = sobrenome;
            Documento = documento;
            Contato = contato;
            ContaCorrente = contaCorrente;
        }

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
        public List<DocumentoImagensAddImagemResponse> Imagens { get; set; }
        public Status Status { get; set; }

    }

    public class DocumentoImagensAddImagemResponse
    {
        public string UrlImagem { get; set; }
        public string NomeImagem { get; set; }
        public Status Status { get; set; }

    }

}