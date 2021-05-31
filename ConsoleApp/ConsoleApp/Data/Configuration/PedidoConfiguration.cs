using ConsoleApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp.Data.Configuration
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
           builder.ToTable("Pedido");
           builder.HasKey(_ => _.Id);
           builder.Property(_ => _.IniciadoEm).HasDefaultValueSql("GETDATE()").ValueGeneratedOnAdd();
           builder.Property(_ => _.StatusPedido).HasConversion<string>();
           builder.Property(_ => _.TipoFrete).HasConversion<string>();
           builder.Property(_ => _.Observacao).HasColumnType("VARCHAR(512)");
           
           builder.HasMany(_ => _.Itens)
            .WithOne(_ => _.Pedido)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
