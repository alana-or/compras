using Compras.API.Migrations;
using Compras.API.Teste.Integracao.ConfiguradoresBancoDeDados;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Compras.API.Teste.Integracao
{
    [SetUpFixture]
    public class TestesFixture
    {
        private WebApplicationFactory<Startup> webAppFactory;
        public static string stringConexaoBancoDeDadosCompras;
        private GerenciadorBancoDeDados gerenciadorBancoDeDados;
        private ConfiguradorCompras configuradorCompras;

        public static readonly Checkpoint CheckpointConfiguradorCompras = new Checkpoint
        {
            DbAdapter = DbAdapter.SqlServer,
        };

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var arquivosDeConfiguracao = new List<(string, string)>
            {
                ("appsettings", ""),
                ("appsettings", environment)
            };

            var configuration = new Configurador().Criar(arquivosDeConfiguracao);
            var stringConexaoServidor = configuration.GetConnectionString("API:SqlServerConnection");
            var diretorioBancoDeDados = configuration.GetSection("DatabasesDirectory").Value;
            TesteIntegracaoBase.Configuration = configuration;

            TesteIntegracaoBase.WebAppFactory = webAppFactory;
            TesteIntegracaoBase.StringConexaoServidor = stringConexaoServidor;
            gerenciadorBancoDeDados = new GerenciadorBancoDeDados(stringConexaoServidor, diretorioBancoDeDados);

            await CriarBancosDeDados(configuration);
        }

        private async Task CriarBancosDeDados(IConfiguration configuration)
        {
            configuradorCompras = new ConfiguradorCompras(webAppFactory, gerenciadorBancoDeDados, configuration);

            stringConexaoBancoDeDadosCompras = configuradorCompras.StringConexaoBancoDeDados;

            var configuradoresBancosDeDados = new List<ConfiguradorBancoDeDados>
            {
                configuradorCompras
            }.OrderBy(x => x.Ordem);

            foreach (var configuradorBancosDeDados in configuradoresBancosDeDados)
            {
                await configuradorBancosDeDados.MigrarEAplicarSeed();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            configuradorCompras?.Limpar();
        }
    }
}
