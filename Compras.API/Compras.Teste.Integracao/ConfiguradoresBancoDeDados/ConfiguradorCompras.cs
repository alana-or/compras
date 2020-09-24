using Compras.API.Migrations;
using Compras.API.Seed;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace Compras.API.Teste.Integracao.ConfiguradoresBancoDeDados
{
    public class ConfiguradorCompras : ConfiguradorBancoDeDados
    {
        public ConfiguradorCompras(WebApplicationFactory<Startup> webAppFactory, 
            GerenciadorBancoDeDados gerenciadorBancoDeDados,
            IConfiguration configuration) : base(webAppFactory, gerenciadorBancoDeDados,
            configuration)
        {
            Ordem = 5;
            BancoDeDados = "Compras";
            StringConexaoBancoDeDados = configuration.GetConnectionString($"API:{BancoDeDados}");
            migrationTagBancoDeDados = nameof(Migrations);
        }
        
        //todo: add seed
        protected override Task AplicarSeedBancoDeDados(ISeedBancoDeDados seedBancoDeDados)
        {
            throw new System.NotImplementedException();
        }

        protected override ISeedBancoDeDados ObterSeedBancoDeDados()
            => scope.ServiceProvider.GetRequiredService<ComprasSeed>();
    }
}
