using System;
using System.Threading.Tasks;
using Features.Tests;
using Modalmais.API.MVC;
using Modalmais.Test.Tests.Config;
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
            // Arrange
             // Act
             var postResponse = await _testsFixture.Client.GetAsync("/api/v1/clientes");

             // Assert
             postResponse.EnsureSuccessStatusCode();
        }
    }
}