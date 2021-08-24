using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using Modalmais.API.DTOs;
using Modalmais.API.MVC;
using Modalmais.Business.Models.Enums;
using Modalmais.Test.Tests.Config;
using Newtonsoft.Json;
using Xunit;

namespace Modalmais.Test.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ClienteDocumentoApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public ClienteDocumentoApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar envio de documento válido ou invalido")]
        public async void NovoDocumento_DocumentoValido_DeveRetornaStatus200AtivarContaEDocumento_PodeRetornarImagemNaoValidaStatus400()
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];
            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
            await using var stream = File.OpenRead("C:/imagens/pequena.png");
            using var content = new MultipartFormDataContent
            {
            // file
            { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
            { new StringContent(cliente.Documento.CPF), "CPF"},
            { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
            { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
            { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
            };

            request.Content = content;


            //Act https://localhost:5001/api/v1/clientes/6122e1a46d1e0ab1e9798f6d/documentos
            var postResponse = await _testsFixture.Client.SendAsync(request);
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteAdicionarDocumentoResponse>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert

            if (!response.Success)
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.Contains("A imagem do documento não é válida.", response.Errors);
                Assert.DoesNotContain("Formato de dado inválido.", response.Errors);
                Assert.DoesNotContain("O cliente não foi encontrado.", response.Errors);
            }
            else
            {
                postResponse.EnsureSuccessStatusCode();
                Assert.Equal(Status.Ativo, response.Data.ContaCorrente.Status);
                Assert.Equal(Status.Ativo, response.Data.Documento.Status);
            }
        }
/*
        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente inválido")]
        public async void NovoCliente_ClienteInvalido_DeveRetornaStatus400()
        {
            // Arrange
            var cliente = _testsFixture.GerarClienteIncorreto();
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("/api/v1/clientes", cliente);
            var responseText = await postResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<Cliente>>(responseText);
            // Assert

        }*/


    }
}