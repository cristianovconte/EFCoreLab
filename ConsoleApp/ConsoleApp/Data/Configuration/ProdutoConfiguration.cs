using ConsoleApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp.Data.Configuration
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produto");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.CodigoBarras).HasColumnType("VARCHAR(12)").IsRequired();
            builder.Property(_ => _.Descricao).HasColumnType("VARCHAR(60)");
            builder.Property(_ => _.Valor).IsRequired();
            builder.Property(_ => _.TipoProduto).HasConversion<string>();
        }
    }
}
