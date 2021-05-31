using ConsoleApp.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleApp.Data.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Cliente");
            builder.HasKey(_ => _.Id);
            builder.Property(_ => _.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(_ => _.Telefone).HasColumnType("CHAR(11)").IsRequired();
            builder.Property(_ => _.CEP).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(_ => _.Estado).HasColumnType("CHAR(2)").IsRequired();
            builder.Property(_ => _.Cidade).HasMaxLength(60).IsRequired();
            builder.HasIndex(_ => _.Telefone).HasName("IDX_CLIENTE_TELEFONE");
        }
    }
}
