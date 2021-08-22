using Microsoft.AspNetCore.Http;
using Modalmais.Business.Models.Enums;
using System;

namespace Modalmais.Business.Models.ObjectValues
{
    public class Documento
    {
        public Documento(string CPF)
        {
            this.CPF = CPF;
            Status = Status.Inativo;
        }

        public string CPF { get; private set; }
        public string? UrlImagem { get; private set; }
        public string? NomeImagem { get; private set; }
        public DateTime? DataEnvio { get; private set; }
        public DateTime? DataValidacao { get; private set; }
        public Status Status { get; private set; }


        public void DataImagemRecebida()
        {
            DataEnvio = DateTime.Now;
        }

        public void DataImagemValidacao()
        {
            DataValidacao = DateTime.Now;
        }

        public void AtribuirImagemDocumento(string urlImagem, string nomeImagem)
        {
            UrlImagem = urlImagem;
            NomeImagem = nomeImagem;
            Status = Status.Ativo;
        }

        public bool ImagemDocumentoValidar(IFormFile documentorecebido)
        {
            DataImagemRecebida();

            var validacao = new Random().Next(1, 3) % 2 == 0;

            if (validacao) DataImagemValidacao();

            return validacao;
        }

    }
}
