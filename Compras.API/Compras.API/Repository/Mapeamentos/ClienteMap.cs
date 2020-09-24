using Compras.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.API.Repository.Mapeamentos
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente", "dbo");

            builder.HasKey(e => e.CodigoCliente)
                    .HasName("Pk_Cliente");

            builder.Property(e => e.CodigoCliente).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Nome).HasColumnName("nome");
            builder.Property(e => e.DataCriacao).HasColumnName("criadoEm");

            builder.HasMany(x => x.Pedidos)
                .WithOne(x => x.Cliente)
                .HasForeignKey(x => x.CodigoPedido);
        }
    }
}
