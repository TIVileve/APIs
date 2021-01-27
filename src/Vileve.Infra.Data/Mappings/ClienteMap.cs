using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            // Primary Key.
            builder.HasKey(c => c.Id);

            // Properties.
            builder.Property(c => c.CreationDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate);

            builder.Property(c => c.Cpf)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.NomeCompleto)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.DataNascimento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.TelefoneFixo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.TelefoneCelular)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            // Table & Column Mappings.
            builder.ToTable("Clientes");

            builder.HasOne(c => c.Consultor)
                .WithMany()
                .HasForeignKey(c => c.ConsultorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}