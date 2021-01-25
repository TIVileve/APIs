using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class ClienteProdutoMap : IEntityTypeConfiguration<ClienteProduto>
    {
        public void Configure(EntityTypeBuilder<ClienteProduto> builder)
        {
            // Primary Key.
            builder.HasKey(cp => cp.Id);

            // Properties.
            builder.Property(cp => cp.CreationDate)
                .IsRequired();

            builder.Property(cp => cp.UpdateDate);

            builder.Property(cp => cp.CodigoProduto)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            // Table & Column Mappings.
            builder.ToTable("ClientesProdutos");

            builder.HasOne(cp => cp.Cliente)
                .WithOne()
                .HasForeignKey<ClienteProduto>(cp => cp.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}