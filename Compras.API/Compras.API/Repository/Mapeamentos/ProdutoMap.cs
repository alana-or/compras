using Compras.API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Compras.API.Repository.Mapeamentos
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto", "dbo");

            builder.HasKey(e => e.CodigoProduto)
                    .HasName("Pk_Produto");

            builder.Property(e => e.CodigoProduto).HasColumnName("id").ValueGeneratedOnAdd();
            builder.Property(e => e.Nome).HasColumnName("nome");
            builder.Property(e => e.Preco).HasColumnName("preco");
            builder.Property(e => e.Descricao).HasColumnName("descricao");
            builder.Property(e => e.Ativo).HasColumnName("ativo");
            builder.Property(e => e.DataCriacao).HasColumnName("criadoEm");

        }
    }
}
