using CommandLine;
using Microsoft.Extensions.Configuration;
using Compras.API.Migrations.BancosDeDados;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Compras.API.Migrations
{
    public class Program
    {
        private static void Main(string[] args) =>
            Parser.Default.ParseArguments<Argumentos>(args)
            .WithParsed(argumentos =>
            {
                if (argumentos.Steps == 0)
                    argumentos.Steps = 1;

                Executar(argumentos);
            });

        public static void Executar(Argumentos argumentos)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var arquivosDeConfiguracao = new List<(string, string)>
            {
                ("appsettings", ""),
                ("appsettings", environment)
            };

            var configurador = new Configurador().Criar(arquivosDeConfiguracao);

            var gerenciadorBancoDeDados = new GerenciadorBancoDeDados(configurador.GetConnectionString("Migrations:SqlServerConnection"),
                configurador.GetSection("DatabasesDirectory").Value);

            var bancosDeDados = new List<BancoDeDados>
            {
                new BancoCompras(),
            };

            var tags = environment == "Local" ? "Development" : environment;

            bancosDeDados
                .OrderBy(x => x.Ordem)
                .Where(x => x.PodeMigrar(argumentos.NomesDosBancos))
                .ToList()
                .ForEach(x => x.Migrar(gerenciadorBancoDeDados, configurador, argumentos, tags));
        }
    }
}
