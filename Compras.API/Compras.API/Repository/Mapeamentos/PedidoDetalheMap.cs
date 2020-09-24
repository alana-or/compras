using Compras.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.API.Repository.Mapeamentos
{
    public class PedidoDetalheMap : IEntityTypeConfiguration<PedidoDetalhe>
    {
        public void Configure(EntityTypeBuilder<PedidoDetalhe> builder)
        {
            builder.ToTable("PedidoDetalhe", "dbo");

            builder.HasKey(e => e.CodigoPedidoDetalhe)
                    .HasName("Pk_PedidoDetalhe");

            builder.Property(e => e.CodigoPedidoDetalhe).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.CodigoPedido).HasColumnName("idPedido");

            builder.HasOne(x => x.Pedido)
                .WithOne(x => x.PedidoDetalhe)
                .HasForeignKey<Pedido>(x=>x.CodigoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
