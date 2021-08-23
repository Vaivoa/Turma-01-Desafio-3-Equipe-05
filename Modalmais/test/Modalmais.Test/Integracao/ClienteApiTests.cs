using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Features.Tests;
using Modalmais.API.MVC;
using Modalmais.Business.Models;
using Modalmais.Test.Tests.Config;
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

        [Fact(DisplayName = "Obter todos clientes"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Clientes")]
        public async Task ObterTodos_DeveRetornarComSucesso()
        {
            //Act && Arrange
            var postResponse = await _testsFixture.Client.GetAsync("/api/v1/clientes");
            var response = JsonConvert.DeserializeObject
                    <IEnumerable<Cliente>>(postResponse.Content.ReadAsStringAsync().Result);
             // Assert
            postResponse.EnsureSuccessStatusCode();
            Assert.Equal(2, response.Count());
        }
    }
    public class ResponseBase<T>
    {
        public HttpStatusCode StatusCode { get; private set; }
        public bool Success { get; private set; }
        public T Data { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
    }
}