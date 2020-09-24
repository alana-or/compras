using Compras.API.Repository.DbContexts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace Compras.API.Teste.Integracao
{
    public class TesteIntegracaoBase
    {
        private IServiceScope applicationScope;
        protected IServiceProvider serviceProvider;
        private readonly IList<IServiceScope> contextScopes;

        public TesteIntegracaoBase() => contextScopes = new List<IServiceScope>();

        public static WebApplicationFactory<Startup> WebAppFactory { get; set; }
        public static string StringConexaoServidor { get; set; }
        public static IConfiguration Configuration { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            applicationScope = WebAppFactory.Server.Host.Services.CreateScope();
            serviceProvider = applicationScope.ServiceProvider;

            var cultureInfo = new CultureInfo("pt-BR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
            CultureInfo.CurrentCulture = cultureInfo;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => applicationScope.Dispose();

        [SetUp]
        public virtual async Task Setup()
        {
            await TestesFixture.CheckpointConfiguradorCompras.Reset(TestesFixture.stringConexaoBancoDeDadosCompras);
        }

        [TearDown]
        public virtual void TearDown()
        {
            foreach (var contextScope in contextScopes)
            {
                contextScope.Dispose();
            }
        }
        
        public ComprasContext ObterCorporateContext()
        {
            var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope();
            contextScopes.Add(scope);
            return scope.ServiceProvider.GetRequiredService<ComprasContext>();
        }
    }
}
