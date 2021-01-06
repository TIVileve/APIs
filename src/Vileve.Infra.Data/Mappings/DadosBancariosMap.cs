using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class DadosBancariosMap : IEntityTypeConfiguration<DadosBancarios>
    {
        public void Configure(EntityTypeBuilder<DadosBancarios> builder)
        {
            // Primary Key.
            builder.HasKey(db => db.Id);

            // Properties.
            builder.Property(db => db.CreationDate)
                .IsRequired();

            builder.Property(db => db.UpdateDate);

            builder.Property(db => db.CodigoBanco)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(db => db.Agencia)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(db => db.ContaSemDigito)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(db => db.Digito)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(db => db.TipoConta)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            // Table & Column Mappings.
            builder.ToTable("DadosBancarios");

            builder.HasOne(db => db.Consultor)
                .WithOne(c => c.DadosBancarios)
                .HasForeignKey<DadosBancarios>(db => db.ConsultorId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}