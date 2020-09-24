using Compras.API.Repository.DbContexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Compras.API.Teste.Integracao
{
    public class WebApplicationFactoryDeTestes<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {

        public WebApplicationFactoryDeTestes() { }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var testingEnvironment = environment == "Local"
                ? "LocalTesting"
                : "Testing";

            builder.UseEnvironment(testingEnvironment);

            builder.ConfigureServices(services =>
            {
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .BuildServiceProvider();

                services.AddDbContext<ComprasContext>(options =>
                {
                    options.UseSqlServer(TestesFixture.stringConexaoBancoDeDadosCompras);
                    options.UseInternalServiceProvider(serviceProvider);
                });
            });
        }
    }
}
