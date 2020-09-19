using Compras.API.Repository.DbContexts;

namespace Compras.API.Repository
{
    public interface IComprasRepository
    {

    }

    public class ComprasRepository : IComprasRepository
    {
        private readonly ComprasContext context;

        public ComprasRepository(ComprasContext context) => this.context = context;
    }
}
