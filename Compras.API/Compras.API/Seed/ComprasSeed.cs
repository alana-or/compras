using Bogus;
using Compras.API.Repository.DbContexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Compras.API.Seed
{
    public class ComprasSeed : ISeedBancoDeDados
    {
        private readonly ComprasContext context;
        private readonly IConfiguration config;
        private readonly Faker faker;

        public ComprasSeed(ComprasContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
            faker = new Faker("pt_BR");
        }

        public async Task AplicarSeed()
        {
            await CriarStatus();
        }

        private Task CriarStatus()
        {
            throw new NotImplementedException();
        }
    }
}
