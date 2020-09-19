using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Compras.API.Migrations
{
    public class Configurador
    {
        public IConfiguration Criar(string arquivoDeConfiguracao) => new ConfigurationBuilder()
            .AddEnvironmentVariables()
            .AddJsonFile($"{arquivoDeConfiguracao}.json")
            .Build();

        public IConfiguration Criar(string arquivoDeConfiguracao, string diretorio) =>
            new ConfigurationBuilder()
                .SetBasePath(diretorio)
                .AddEnvironmentVariables()
                .AddJsonFile($"{arquivoDeConfiguracao}.json")
                .Build();

        public IConfiguration Criar(
            IEnumerable<(string nome, string ambiente)> arquivosDeConfiguracao)
            => new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFiles(ObterNomesArquivosConfiguracao(arquivosDeConfiguracao))
                .Build();

        public IConfiguration Criar(
            IEnumerable<(string nome, string ambiente)> arquivosDeConfiguracao, string diretorio)
            => new ConfigurationBuilder()
                .SetBasePath(diretorio)
                .AddEnvironmentVariables()
                .AddJsonFiles(ObterNomesArquivosConfiguracao(arquivosDeConfiguracao))
                .Build();

        private IEnumerable<string> ObterNomesArquivosConfiguracao(
            IEnumerable<(string nome, string ambiente)> arquivosDeConfiguracao)
            => arquivosDeConfiguracao
                .Select(arquivo => string.IsNullOrEmpty(arquivo.ambiente)
                    ? arquivo.nome
                    : $"{arquivo.nome}.{arquivo.ambiente}")
                .Select(nomeArquivo => $"{nomeArquivo}.json")
                .Where(x => File.Exists(Path.Combine(AppContext.BaseDirectory, x)))
                .ToList();
    }

    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddJsonFiles(this IConfigurationBuilder builder,
            IEnumerable<string> files)
        {
            foreach (var file in files)
                builder.AddJsonFile(file);

            return builder;
        }
    }
}
