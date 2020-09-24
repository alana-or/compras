using Compras.API.Migrations;
using Compras.API.Seed;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Compras.API.Teste.Integracao.ConfiguradoresBancoDeDados
{
    public abstract class ConfiguradorBancoDeDados : IDisposable
    {
        public int Ordem { get; protected set; }
        private const string SufixoBancoDeDados = ".Testes";
        public string BancoDeDados { get; protected set; }
        public string StringConexaoBancoDeDados { get; protected set; }
        protected string migrationTagBancoDeDados;

        protected readonly WebApplicationFactory<Startup> webAppFactory;
        protected readonly GerenciadorBancoDeDados gerenciadorBancoDeDados;
        protected readonly IConfiguration configuration;
        protected IServiceScope scope;

        private const string migrationTagAmbienteDesenvolvimento = "Development";

        public ConfiguradorBancoDeDados(WebApplicationFactory<Startup> webAppFactory,
            GerenciadorBancoDeDados gerenciadorBancoDeDados,
            IConfiguration configuration)
        {
            this.gerenciadorBancoDeDados = gerenciadorBancoDeDados;
            this.configuration = configuration;
            scope = webAppFactory.Server.Host.Services.CreateScope();
        }

        public async Task MigrarEAplicarSeed()
        {
            Migrar();
            await AplicarSeed();
        }

        private void Migrar()
        {
            gerenciadorBancoDeDados.CriarCasoNaoExista($"{BancoDeDados}{SufixoBancoDeDados}");

            var migrationsService = new MigrationsService();

            var provedorDeServicos = migrationsService.CriarServicos(StringConexaoBancoDeDados,
                SufixoBancoDeDados,
                migrationTags: new[]
                {
                    migrationTagBancoDeDados,
                    migrationTagAmbienteDesenvolvimento
                });

            using (var escopo = provedorDeServicos.CreateScope())
                migrationsService.AtualizarBancoDeDados(escopo.ServiceProvider);
        }

        private async Task AplicarSeed()
        {
            var seedBancoDeDados = ObterSeedBancoDeDados();
            await AplicarSeedBancoDeDados(seedBancoDeDados);
        }

        protected abstract ISeedBancoDeDados ObterSeedBancoDeDados();

        protected abstract Task AplicarSeedBancoDeDados(ISeedBancoDeDados seedBancoDeDados);

        public void Limpar() => gerenciadorBancoDeDados.ExcluirBancoDeDados($"{BancoDeDados}{SufixoBancoDeDados}");

        public void Dispose() => scope?.Dispose();
    }
}
