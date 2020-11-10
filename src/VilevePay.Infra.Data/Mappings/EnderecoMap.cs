using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VilevePay.Domain.Models;

namespace VilevePay.Infra.Data.Mappings
{
    public class EnderecoMap : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            // Primary Key.
            builder.HasKey(e => e.Id);

            // Properties.
            builder.Property(e => e.CreationDate)
                .IsRequired();

            builder.Property(e => e.UpdateDate);

            builder.Property(e => e.TipoEndereco);

            builder.Property(e => e.Cep)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Logradouro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Numero);

            builder.Property(e => e.Complemento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Bairro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Cidade)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Estado)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Principal);

            builder.Property(e => e.ComprovanteBase64)
                .HasColumnType("varchar(MAX)");

            // Table & Column Mappings.
            builder.ToTable("Enderecos");

            builder.HasOne(e => e.Consultor)
                .WithMany(c => c.Enderecos)
                .HasForeignKey(e => e.ConsultorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}