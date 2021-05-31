using ConsoleApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp.Data.Configuration
{
    public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
    {
        public void Configure(EntityTypeBuilder<PedidoItem> builder)
        {
            builder.ToTable("PedidoItem");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Quantitade).HasDefaultValue(1).IsRequired();
            builder.Property(_ => _.Valor).IsRequired();
            builder.Property(_ => _.Desconto).IsRequired();
        }
    }
}
