using Modalmais.API.DTOs;
using Modalmais.API.Extensions;
using Modalmais.API.MVC;
using Modalmais.Business.Models;
using Modalmais.Core.Models.Enums;
using Modalmais.Test.Tests;
using Modalmais.Test.Tests.Config;
using Modalmais.Transacoes.API.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Xunit;


namespace Modalmais.Test
{
    [TestCaseOrderer("Modalmais.Test.PriorityOrderer", "Modalmais.Test")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ClienteApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public ClienteApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar Obter todos clientes"), TestPriority(2)]
        public async void ObterCliente_ListaClientes_DeveRetornaStatus200()
        {
            // Arrange
            var postResponse = await _testsFixture.Client.GetAsync("/api/v1/clientes");
            //Act
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<IList<ClienteResponse>>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.True(response.Success);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Single(response.Data);
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente válido"), TestPriority(1)]
        public async void NovoCliente_ClienteValido_DeveRetornaStatus200()
        {
            // Arrange
            var cliente = _testsFixture.GerarClienteValido();
            while (cliente.Documento.CPF == "61492511013")
            {
                cliente = _testsFixture.GerarClienteValido();
            }

            var postResponse = await _testsFixture.Client.PostAsJsonAsync("/api/v1/clientes", cliente);

            //Act
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<Cliente>>(postResponse.Content.ReadAsStringAsync().Result);

            // Assert
            Assert.Equal(cliente.Documento.CPF, response.Data.Documento.CPF);
            Assert.Equal(Status.Inativo, response.Data.Documento.Status);
            Assert.Null(response.Data.ContaCorrente.ChavePix);
            Assert.True(response.Data.Documento.Imagens.Count() == 0);
            Assert.Equal(cliente.Nome, response.Data.Nome);
            Assert.Equal(cliente.Contato.Celular.DDD, response.Data.Contato.Celular.DDD);
            Assert.Equal(cliente.Contato.Celular.Numero, response.Data.Contato.Celular.Numero);
            Assert.Equal(cliente.Contato.Email, response.Data.Contato.Email);
            Assert.Equal("746", response.Data.ContaCorrente.Banco);
            Assert.Equal("0001", response.Data.ContaCorrente.Agencia);
            Assert.NotEmpty(response.Data.ContaCorrente.Numero);
            Assert.Equal(Status.Inativo, response.Data.ContaCorrente.Status);
            Assert.Equal(cliente.Contato.Celular.Numero, response.Data.Contato.Celular.Numero);
            Assert.Equal(cliente.Contato.Email, response.Data.Contato.Email);
            Assert.Equal(cliente.Nome, response.Data.Nome);
            Assert.Equal(cliente.Sobrenome, response.Data.Sobrenome);
            Assert.True(response.Success);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Null(response.Errors);
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente inválido"), TestPriority(0)]
        public async void NovoCliente_ClienteInvalido_DeveRetornaStatus400()
        {
            // Arrange
            var cliente = _testsFixture.GerarClienteIncorreto();
            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("/api/v1/clientes", cliente);
            var responseText = await postResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(responseText);
            // Assert


            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("A requisição é inválida", response.Errors);
            Assert.Null(response.Data);
            Assert.True(response.Errors.Count() > 1);
        }



        [Theory(DisplayName = "Validar envio de documento com validação randomica."), TestPriority(5)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [Trait("Categoria", "Testes Integracao Cliente")]
        public async void NovoDocumentoImagem_DocumentoValido_DeveRetornaStatus201AtivarContaEDocumento_PodeRetornarImagemNaoValidaStatus400(int numeroImage)
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];


            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
            await using var stream = File.OpenRead(@"../../../imgs_para_teste/pequena.png");
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
                { new StringContent(cliente.Documento.CPF), "CPF"},
                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
                { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
            };
            request.Content = content;


            //Act
            var postResponse = await _testsFixture.Client.SendAsync(request);
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);



            // Assert

            if (!response.Success)
            {
                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
                Assert.Contains("A imagem do documento não é válida.", response.Errors);
                Assert.Null(response.Data);
                Assert.Single(response.Errors);
            }
            else
            {
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.Equal(Status.Ativo, response.Data.ContaCorrente.Status);
                Assert.Equal(Status.Ativo, response.Data.Documento.Status);
                Assert.Equal(Status.Ativo, response.Data.Documento.Imagens.Last().Status);
                Assert.Equal("pequena.png", response.Data.Documento.Imagens.Last().NomeImagem);

                var imagensDesativadas = response.Data.Documento.Imagens.Where(o => o.Status == Status.Desativado);
                Assert.Equal(response.Data.Documento.Imagens.Count() - 1, imagensDesativadas.Count());
            }
        }



        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar envio de documento com imagem inválida."), TestPriority(4)]
        public async void NovoDocumentoImagem_ImagemInvalida_DeveRetornaStatus400ErroImagemInvalida()
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];


            using var request = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
            await using var stream = File.OpenRead(@"../../../imgs_para_teste/grande.jpg");
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(stream), "ImagemDocumento", "grande.jpg" },
                { new StringContent(cliente.Documento.CPF), "CPF"},
                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
                { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
            };
            request.Content = content;


            //Act
            var postResponse = await _testsFixture.Client.SendAsync(request);
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);



            // Assert

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains("A requisição é inválida", response.Errors);
            Assert.Contains(MaxFileSizeAttribute.MsgErro, response.Errors);
            Assert.Contains(expected: AllowedExtensionsAttribute.MsgErro, response.Errors);
            Assert.Equal(3, response.Errors.Count());

        }



        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar envio de documento sem permissão."), TestPriority(3)]
        public async void NovoDocumentoImagem_AcessoNegado_DeveRetornaStatus403PermissaoNegada()
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];


            using var requestNumeroConta = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
            await using var stream = File.OpenRead(@"../../../imgs_para_teste/pequena.png");
            using var content = new MultipartFormDataContent
            {
                { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
                { new StringContent(cliente.Documento.CPF), "CPF"},
                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
                { new StringContent("1111111111111111"), "Numero"},
            };
            requestNumeroConta.Content = content;


            using var requestCpf = new HttpRequestMessage(HttpMethod.Post, $"api/v1/clientes/{cliente.Id}/documentos");
            using var content2 = new MultipartFormDataContent
            {
                { new StreamContent(stream), "ImagemDocumento", "pequena.png" },
                { new StringContent("61492511013"), "CPF"},
                { new StringContent(cliente.ContaCorrente.Agencia), "Agencia"},
                { new StringContent(cliente.ContaCorrente.Banco), "Banco"},
                { new StringContent(cliente.ContaCorrente.Numero), "Numero"},
            };
            requestCpf.Content = content2;



            //Act
            var postResponse = await _testsFixture.Client.SendAsync(requestNumeroConta);
            var responseNumeroConta = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);


            postResponse = await _testsFixture.Client.SendAsync(requestCpf);
            var responseCpf = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);


            // Assert

            Assert.Equal(HttpStatusCode.Forbidden, responseNumeroConta.StatusCode);
            Assert.Contains("Permissão negada", responseNumeroConta.Errors);
            Assert.Single(responseNumeroConta.Errors);


            Assert.Equal(HttpStatusCode.Forbidden, responseCpf.StatusCode);
            Assert.Contains("Permissão negada", responseCpf.Errors);
            Assert.Single(responseCpf.Errors);

        }



        [InlineData(TipoChavePix.Aleatoria, "")]
        [InlineData(TipoChavePix.Email, "asdasd@gmail.com")]
        [InlineData(TipoChavePix.Telefone, "17956235958")]
        [InlineData(TipoChavePix.CPF, "")]
        [Trait("Categoria", "Testes Integracao Cliente")]
        [Theory(DisplayName = "Validar cadastro pix valido."), TestPriority(6)]
        public async void NovoPix_PixValido_DeveRetornaStatus201PixAtivo(TipoChavePix tipoPix, string chave)
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];
            var chavePix = new ChavePixRequest
            {
                Chave = chave,
                Tipo = tipoPix,

            };
            if (tipoPix == TipoChavePix.CPF)
            {
                chavePix.Chave = cliente.Documento.CPF;
            }
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync($"api/v1/clientes/{cliente.Id}/chavepix", chavePix);
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert
            Assert.Equal(cliente.Documento.CPF, response.Data.Documento.CPF);
            Assert.NotNull(response.Data.ContaCorrente.ChavePix);
            Assert.Equal(Status.Ativo, response.Data.ContaCorrente.ChavePix.Ativo);
            Assert.True(response.Success);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.Null(response.Errors);
            if (tipoPix == TipoChavePix.CPF)
            {
                Assert.Equal(cliente.Documento.CPF, response.Data.ContaCorrente.ChavePix.Chave);
            }
            if (tipoPix == TipoChavePix.Aleatoria)
            {
                Assert.Equal(32, response.Data.ContaCorrente.ChavePix.Chave.Length);
            }
        }

        [InlineData(TipoChavePix.Email, "")]
        [InlineData(TipoChavePix.Telefone, "")]
        [InlineData(TipoChavePix.Telefone, "4521518")]
        [InlineData(TipoChavePix.CPF, "61492511013")]
        [InlineData((TipoChavePix)5, "asdasd")]
        [InlineData(TipoChavePix.Email, "@email.com")]
        [Trait("Categoria", "Testes Integracao Cliente")]
        [Theory(DisplayName = "Validar cadastro pix invalido."), TestPriority(7)]
        public async void NovoPix_PixInvalido_DeveRetornaStatus400PixAtivo(TipoChavePix tipoPix, string chave)
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];
            var chavePix = new ChavePixRequest
            {
                Chave = chave,
                Tipo = tipoPix,

            };
            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync($"api/v1/clientes/{cliente.Id}/chavepix", chavePix);
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(response.Success);

        }




        [InlineData(TipoChavePix.Aleatoria, "")]
        [InlineData(TipoChavePix.Email, "asdasd@gmail.com")]
        [InlineData(TipoChavePix.Telefone, "17956235958")]
        [InlineData(TipoChavePix.CPF, "")]
        [Trait("Categoria", "Testes Integracao Cliente")]
        [Theory(DisplayName = "Verificar se a chave pix possui uma conta relacionada."), TestPriority(8)]
        public async void ObterContaPelaChavePix_PixExistente_DeveRetornaStatus200EContaRelacionada(TipoChavePix tipoPix, string chave)
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];

            var chavePix = new ChavePixRequest { Chave = chave, Tipo = tipoPix };

            if (tipoPix == TipoChavePix.CPF) chavePix.Chave = cliente.Documento.CPF;

            var postResponse = await _testsFixture.Client.PostAsJsonAsync($"api/v1/clientes/{cliente.Id}/chavepix", chavePix);
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<ClienteResponse>>(postResponse.Content.ReadAsStringAsync().Result);


            if (tipoPix == TipoChavePix.Aleatoria)
            {
                getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
                clientesResponse = JsonConvert.DeserializeObject
                       <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
                cliente = clientesResponse.Data[0];
            }


            // Act


            if (tipoPix == TipoChavePix.Aleatoria) chavePix.Chave = cliente.ContaCorrente.ChavePix.Chave;



            var postResponseContaPix = await _testsFixture.Client.GetAsync($"api/v1/clientes/contas/chavepix?" +
                                                                                    $"chave={chavePix.Chave}&" +
                                                                                    $"tipo={(int)chavePix.Tipo}");
            var responseContaPix = JsonConvert.DeserializeObject
                    <ResponseBase<ContaPixResponse>>(postResponseContaPix.Content.ReadAsStringAsync().Result);



            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.Equal(HttpStatusCode.OK, responseContaPix.StatusCode);
            Assert.True(responseContaPix.Success);
            Assert.Null(responseContaPix.Errors);
            Assert.Equal(cliente.Nome, responseContaPix.Data.Nome);
            Assert.Equal(cliente.Sobrenome, responseContaPix.Data.Sobrenome);
            Assert.Equal(cliente.ContaCorrente.Banco, responseContaPix.Data.ContaCorrente.Banco);

            if (tipoPix == TipoChavePix.CPF)
                Assert.Equal(cliente.Documento.CPF, responseContaPix.Data.ContaCorrente.ChavePix.Chave);
            if (tipoPix == TipoChavePix.Email)
                Assert.Equal(chave, responseContaPix.Data.ContaCorrente.ChavePix.Chave);
            if (tipoPix == TipoChavePix.Telefone)
                Assert.Equal(chave, responseContaPix.Data.ContaCorrente.ChavePix.Chave);
            if (tipoPix == TipoChavePix.Aleatoria)
                Assert.Equal(cliente.ContaCorrente.ChavePix.Chave, responseContaPix.Data.ContaCorrente.ChavePix.Chave);

            Assert.Equal(tipoPix, responseContaPix.Data.ContaCorrente.ChavePix.Tipo);
            Assert.True(DateTime.UtcNow > responseContaPix.Data.ContaCorrente.ChavePix.DataCriacao);
            Assert.Equal(Status.Ativo, responseContaPix.Data.ContaCorrente.ChavePix.Ativo);
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Atualizar cadastro de um cliente válido"), TestPriority(9)]
        public async void AtualizarCliente_ClienteValido_DeveRetornaStatus203()
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];

            var dadosAtualizados = _testsFixture.GerarClienteValido();

            //Act
            var putResponse = await _testsFixture.Client.PutAsJsonAsync($"api/v1/clientes/{cliente.Id}", dadosAtualizados);

            // Assert
            Assert.Equal(HttpStatusCode.NoContent, putResponse.StatusCode);
            Assert.True(putResponse.IsSuccessStatusCode);
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Atualizar cadastro de um cliente Inválido"), TestPriority(10)]
        public async void AtualizarCliente_ClienteInvalido_DeveRetornaStatus400()
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];

            var dadosAtualizados = _testsFixture.GerarClienteIncorreto();

            //Act
            var putResponse = await _testsFixture.Client.PutAsJsonAsync($"api/v1/clientes/{cliente.Id}", dadosAtualizados);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, putResponse.StatusCode);
            Assert.False(putResponse.IsSuccessStatusCode);
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Chave Pix Valida"), TestPriority(11)]
        public async void TransacaoPix_ChaveValida_DeveRetornaStatus200()
        {
            // Arrange
            var getResponse = await _testsFixture.Client.GetAsync("api/v1/clientes");
            var clientesResponse = JsonConvert.DeserializeObject
                    <ResponseBase<List<ClienteResponse>>>(getResponse.Content.ReadAsStringAsync().Result);
            var cliente = clientesResponse.Data[0];

            TransacaoRequest transacaoValida = new()
            { Chave = cliente.ContaCorrente.ChavePix.Chave, 
                Tipo = cliente.ContaCorrente.ChavePix.Tipo, 
                Valor = (decimal)5000.00, Descricao = " " };

            //Act
            var postResponse = await _testsFixture.ClientTransacao.PostAsJsonAsync($"api/v1/transacoes", transacaoValida);
            var response = JsonConvert.DeserializeObject
                   <ResponseBase<ContaPixResponse>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
            Assert.Empty(response.Errors);
        }


    }
}