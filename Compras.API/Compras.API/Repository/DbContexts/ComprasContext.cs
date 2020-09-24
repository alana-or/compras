using Compras.API.Domain;
using Compras.API.Repository.Mapeamentos;
using Microsoft.EntityFrameworkCore;

namespace Compras.API.Repository.DbContexts
{
    public class ComprasContext : DbContext
    {
        public ComprasContext()
        {
        }

        public ComprasContext(DbContextOptions<ComprasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Produto> Produtos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }
        public virtual DbSet<PedidoDetalhe> PedidoDetalhes { get; set; }
        public virtual DbSet<Cliente> Clientes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProdutoMap());
        }
    }
}
