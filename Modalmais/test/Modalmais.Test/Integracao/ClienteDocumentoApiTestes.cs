//using Modalmais.API.DTOs;
//using Modalmais.API.Extensions;
//using Modalmais.API.MVC;
//using Modalmais.Business.Models.Enums;
//using Modalmais.Test.Tests.Config;
//using Newtonsoft.Json;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using Xunit;

//namespace Modalmais.Test.Tests
//{
//    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
//    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
//    public class ClienteDocumentoApiTests
//    {
//        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

//        public ClienteDocumentoApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
//        {
//            _testsFixture = testsFixture;
//        }

//        [Trait("Categoria", "Testes Integracao Cliente")]
//        [Fact(DisplayName = "Validar envio de documento com validação randomica.")]
//        public async void NovoDocumentoImagem_DocumentoValido_DeveRetornaStatus200AtivarContaEDocumento_PodeRetornarImagemNaoValidaStatus400()
//        {
//            // Arrange
//            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
//            var clientesResponse = JsonConvert.DeserializeObject
//                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
//            var cliente = clientesResponse.Data[0];


//            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
//            await using var stream = File.OpenRead(@"../../../imgs_para_teste/pequena.png");
//            using var content = new MultipartFormDataContent
//            {
//                // file
//                { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
//                { new StringContent(cliente.Documento.CPF), "CPF"},
//                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
//                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
//                { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
//            };
//            request.Content = content;


//            //Act https://localhost:5001/api/v1/clientes/6122e1a46d1e0ab1e9798f6d/documentos
//            var postResponse = await _testsFixture.Client.SendAsync(request);
//            var response = JsonConvert.DeserializeObject
//                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);



//            // Assert

//            if (!response.Success)
//            {
//                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//                Assert.Contains("A imagem do documento não é válida.", response.Errors);
//                Assert.Null(response.Data);
//                Assert.Single(response.Errors);
//            }
//            else
//            {
//                postResponse.EnsureSuccessStatusCode();
//                Assert.Equal(Status.Ativo, response.Data.ContaCorrente.Status);
//                Assert.Equal(Status.Ativo, response.Data.Documento.Status);
//                Assert.Single(response.Data.Documento.Imagens);
//                Assert.Equal(Status.Ativo, response.Data.Documento.Imagens[0].Status);
//                Assert.Equal("pequena.png", response.Data.Documento.Imagens[0].NomeImagem);
//            }
//        }



//        [Trait("Categoria", "Testes Integracao Cliente")]
//        [Fact(DisplayName = "Validar envio de documento com imagem inválida.")]
//        public async void NovoDocumentoImagem_ImagemInvalida_DeveRetornaStatus400ErroImagemInvalida()
//        {
//            // Arrange
//            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
//            var clientesResponse = JsonConvert.DeserializeObject
//                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
//            var cliente = clientesResponse.Data[0];


//            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
//            await using var stream = File.OpenRead(@"../../../imgs_para_teste/grande.jpg");
//            using var content = new MultipartFormDataContent
//            {
//                { new StreamContent(stream), "ImagemDocumento", "grande.jpg" },
//                { new StringContent(cliente.Documento.CPF), "CPF"},
//                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
//                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
//                { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
//            };
//            request.Content = content;


//            //Act
//            var postResponse = await _testsFixture.Client.SendAsync(request);
//            var response = JsonConvert.DeserializeObject
//                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);



//            // Assert

//            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
//            Assert.Contains("A requisição é inválida", response.Errors);
//            Assert.Contains(MaxFileSizeAttribute.MsgErro, response.Errors);
//            Assert.Contains(AllowedExtensionsAttribute.MsgErro, response.Errors);
//            Assert.Equal(3, response.Errors.Count());

//        }



//        [Trait("Categoria", "Testes Integracao Cliente")]
//        [Fact(DisplayName = "Validar envio de documento sem permissão.")]
//        public async void NovoDocumentoImagem_AcessoNegado_DeveRetornaStatus403PermissaoNegada()
//        {
//            // Arrange
//            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
//            var clientesResponse = JsonConvert.DeserializeObject
//                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
//            var cliente = clientesResponse.Data[0];


//            using var requestNumeroConta = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
//            await using var stream = File.OpenRead(@"../../../imgs_para_teste/pequena.png");
//            using var content = new MultipartFormDataContent
//            {
//                { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
//                { new StringContent(cliente.Documento.CPF), "CPF"},
//                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
//                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
//                { new StringContent("1111111111111111"), "Numero"},
//            };
//            requestNumeroConta.Content = content;


//            using var requestCpf = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
//            using var content2 = new MultipartFormDataContent
//            {
//                { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
//                { new StringContent("61492511013"), "CPF"},
//                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
//                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
//                { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
//            };
//            requestCpf.Content = content2;



//            //Act
//            var postResponse = await _testsFixture.Client.SendAsync(requestNumeroConta);
//            var responseNumeroConta = JsonConvert.DeserializeObject
//                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);


//            postResponse = await _testsFixture.Client.SendAsync(requestCpf);
//            var responseCpf = JsonConvert.DeserializeObject
//                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);


//            // Assert

//            Assert.Equal(HttpStatusCode.Forbidden, responseNumeroConta.StatusCode);
//            Assert.Contains("Permissão negada", responseNumeroConta.Errors);
//            Assert.Single(responseNumeroConta.Errors);


//            Assert.Equal(HttpStatusCode.Forbidden, responseCpf.StatusCode);
//            Assert.Contains("Permissão negada", responseCpf.Errors);
//            Assert.Single(responseCpf.Errors);

//        }
//    }
//}