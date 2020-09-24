using Compras.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.API.Repository.Mapeamentos
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedido", "dbo");

            builder.HasKey(e => e.CodigoPedido)
                    .HasName("Pk_Pedido");

            builder.Property(e => e.CodigoPedido).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Status).HasColumnName("status");
            builder.Property(e => e.DataCriacao).HasColumnName("criadoEm");

            builder.HasOne(x => x.Cliente)
                .WithMany(x => x.Pedidos)
                .HasForeignKey(x=>x.CodigoCliente);
        }
    }
}
