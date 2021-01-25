using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class ClienteEnderecoMap : IEntityTypeConfiguration<ClienteEndereco>
    {
        public void Configure(EntityTypeBuilder<ClienteEndereco> builder)
        {
            // Primary Key.
            builder.HasKey(ce => ce.Id);

            // Properties.
            builder.Property(ce => ce.CreationDate)
                .IsRequired();

            builder.Property(ce => ce.UpdateDate);

            builder.Property(ce => ce.Cep)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(ce => ce.Logradouro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(ce => ce.Numero);

            builder.Property(ce => ce.Complemento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(ce => ce.Bairro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(ce => ce.Cidade)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(ce => ce.Estado)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(ce => ce.ComprovanteBase64)
                .HasColumnType("varchar(MAX)");

            // Table & Column Mappings.
            builder.ToTable("ClientesEnderecos");

            builder.HasOne(ce => ce.Cliente)
                .WithMany()
                .HasForeignKey(ce => ce.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}