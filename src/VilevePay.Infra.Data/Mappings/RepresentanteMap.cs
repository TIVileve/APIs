using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VilevePay.Domain.Models;

namespace VilevePay.Infra.Data.Mappings
{
    public class RepresentanteMap : IEntityTypeConfiguration<Representante>
    {
        public void Configure(EntityTypeBuilder<Representante> builder)
        {
            // Primary Key.
            builder.HasKey(r => r.Id);

            // Properties.
            builder.Property(r => r.CreationDate)
                .IsRequired();

            builder.Property(r => r.UpdateDate);

            builder.Property(r => r.Cpf)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(r => r.NomeCompleto)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(r => r.Sexo);

            builder.Property(r => r.EstadoCivil);

            builder.Property(r => r.Nacionalidade)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(r => r.DocumentoFrenteBase64)
                .HasColumnType("varchar(MAX)");

            builder.Property(r => r.DocumentoVersoBase64)
                .HasColumnType("varchar(MAX)");

            // Table & Column Mappings.
            builder.ToTable("Representantes");

            builder.HasOne(r => r.Consultor)
                .WithOne(c => c.Representante)
                .HasForeignKey<Representante>(r => r.ConsultorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}