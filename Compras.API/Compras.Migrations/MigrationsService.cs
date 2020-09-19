using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;
using FluentMigrator.Runner.Logging;
using FluentMigrator.Runner.Processors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Compras.API.Migrations
{
    public class MigrationsService
    {
        public IServiceProvider CriarServicos(string connectionString,
            string sufixoBancosDeDados,
            Argumentos argumentos = null,
            params string[] migrationTags)
        {
            var service = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(construtor => construtor
                    .AddSqlServer2016()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Program).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole())
                .AddSingleton<IProvedorDeServico>(new ProvedorDeServico
                {
                    SufixoBancosDeDados = sufixoBancosDeDados
                })
                .Configure<RunnerOptions>(o => o.Tags = migrationTags);

            if (argumentos != null && !string.IsNullOrWhiteSpace(argumentos.ArquivoDeSaida))
                service
                    .Configure<ProcessorOptions>(o =>
                    {
                        o.PreviewOnly = true;
                        o.Timeout = new TimeSpan(0, 1, 0);
                    })
                    .Configure<LogFileFluentMigratorLoggerOptions>(o =>
                    {
                        o.ShowSql = true;
                        o.OutputFileName = argumentos.ArquivoDeSaida;

                    })
                    .AddSingleton<ILoggerProvider, LogFileFluentMigratorLoggerProvider>();

            return service.BuildServiceProvider(false);
        }

        public void AtualizarBancoDeDados(IServiceProvider provedorDeServicos, Argumentos argumentos = null)
        {
            var executor = provedorDeServicos.GetRequiredService<IMigrationRunner>();

            if (argumentos != null && argumentos.Rollback)
                executor.Rollback(argumentos.Steps);
            else
                executor.MigrateUp();
        }
    }
}
