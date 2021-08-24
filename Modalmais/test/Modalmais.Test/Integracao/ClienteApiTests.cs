using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Features.Tests;
using Modalmais.API.MVC;
using Modalmais.Business.Models;
using Modalmais.Business.Models.ObjectValues;
using Modalmais.Business.Models.Validation;
using Modalmais.Test.Tests.Config;
using Modalmais.Test.Unitarios;
using Newtonsoft.Json;
using Xunit;

namespace Modalmais.Test.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class ClienteApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public ClienteApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente válido")]
        public async void ObterCliente_ListaClientes_DeveRetornaStatus200()
        {
            // Arrange
            var postResponse = await _testsFixture.Client.GetAsync("/api/v1/clientes");
            //Act
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<IList<Cliente>>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.NotEmpty(response.Data);
        }
        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente válido")]
        public async void NovoCliente_ClienteValido_DeveRetornaStatus200()
        {
            // Arrange
            var cliente = _testsFixture.GerarClienteValido();
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("/api/v1/clientes", cliente);
            //Act
            var response = JsonConvert.DeserializeObject
                    < ResponseBase<Cliente>>(postResponse.Content.ReadAsStringAsync().Result);
            // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(cliente.Documento.CPF, response.Data.Documento.CPF);
        }

        [Trait("Categoria", "Testes Integracao Cliente")]
        [Fact(DisplayName = "Validar criação de um cliente inválido")]
        public async void NovoCliente_ClienteInvalido_DeveRetornaStatus400()
        {
            // Arrange
            var cliente = _testsFixture.GerarClienteIncorreto();
            //Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("/api/v1/clientes", cliente);
            var responseText = await postResponse.Content.ReadAsStringAsync();
            var response = JsonConvert.DeserializeObject
                    <ResponseBase<Cliente>>(responseText);
            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Contains(ClienteValidator.ClientePropriedadeVazia
                .Replace("{PropertyName}",nameof(Cliente.Nome)),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeVazia
                .Replace("{PropertyName}",nameof(Cliente.Sobrenome)),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeVazia
                .Replace("{PropertyName}", $"{nameof(Contato)} {nameof(Contato.Email)}"),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeVazia
                .Replace("{PropertyName}", $"{nameof(Contato)} {nameof(Contato.Celular)} {nameof(Contato.Celular.Numero)}"),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeVazia
                .Replace("{PropertyName}", $"{nameof(Documento)} {nameof(Documento.CPF)}"),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeValida
                .Replace("{PropertyName}", $"{nameof(Documento)} {nameof(Documento.CPF)}"),
                 response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeValida
                .Replace("{PropertyName}", $"{nameof(Contato)} {nameof(Contato.Email)}"),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeCharLimite
                .Replace("{PropertyName}",$"{nameof(Contato)} {nameof(Contato.Email)}")
                .Replace("{MinLength}",ClienteValidator.ClienteEmailMinimoChar.ToString())
                .Replace("{MaxLength}", ClienteValidator.ClienteNomeSobrenomeEmailMaximoChar.ToString()),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeCharLimite
                .Replace("{PropertyName}",$"{nameof(Contato)} {nameof(Contato.Celular)} {nameof(Contato.Celular.Numero)}")
                .Replace("{MinLength}",ClienteValidator.ClienteCelularMinimoMaxChar.ToString())
                .Replace("{MaxLength}", ClienteValidator.ClienteCelularMinimoMaxChar.ToString()),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeCharLimite
                .Replace("{PropertyName}",nameof(Cliente.Nome))
                .Replace("{MinLength}",ClienteValidator.ClienteNomeSobrenomeMinimoChar.ToString())
                .Replace("{MaxLength}", ClienteValidator.ClienteNomeSobrenomeEmailMaximoChar.ToString()),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeCharLimite
                .Replace("{PropertyName}",nameof(Cliente.Sobrenome))
                .Replace("{MinLength}",ClienteValidator.ClienteNomeSobrenomeMinimoChar.ToString())
                .Replace("{MaxLength}", ClienteValidator.ClienteNomeSobrenomeEmailMaximoChar.ToString()),
                response.Errors);
            Assert.Contains(ClienteValidator.ClientePropriedadeCharLimite
                .Replace("{PropertyName}", $"{nameof(Cliente.Documento)} {nameof(Cliente.Documento.CPF)}")
                .Replace("{MinLength}", ClienteValidator.ClienteCpfMinimoMaxChar.ToString())
                .Replace("{MaxLength}", ClienteValidator.ClienteCpfMinimoMaxChar.ToString()),
                response.Errors);
        } 
    }
}