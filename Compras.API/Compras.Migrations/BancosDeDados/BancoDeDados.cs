using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;

namespace Compras.API.Migrations.BancosDeDados
{
    public abstract class BancoDeDados
    {
        protected string nomeDoBanco;
        public int Ordem { get; protected set; }

        public bool PodeMigrar(IEnumerable<string> nomesDoBanco)
            => nomesDoBanco == null || nomesDoBanco != null && (!nomesDoBanco.Any() || nomesDoBanco.Any(x => x == nomeDoBanco));

        public void Migrar(GerenciadorBancoDeDados gerenciadorBancoDeDados,
            IConfiguration configurador,
            Argumentos argumentos,
            params string[] migrationTags)
        {
            var tags = new List<string>
            {
                nomeDoBanco
            };
            tags.AddRange(migrationTags);

            var migrationsService = new MigrationsService();

            gerenciadorBancoDeDados.CriarCasoNaoExista(nomeDoBanco);

            var provedorDeServicos = migrationsService
                .CriarServicos(configurador.GetConnectionString($"Migrations:{nomeDoBanco}"),
                    "",
                    argumentos,
                    tags.ToArray());

            using (var escopo = provedorDeServicos.CreateScope())
                migrationsService.AtualizarBancoDeDados(escopo.ServiceProvider, argumentos);
        }
    }
}
