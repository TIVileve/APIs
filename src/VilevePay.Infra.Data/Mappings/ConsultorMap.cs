using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VilevePay.Domain.Models;

namespace VilevePay.Infra.Data.Mappings
{
    public class ConsultorMap : IEntityTypeConfiguration<Consultor>
    {
        public void Configure(EntityTypeBuilder<Consultor> builder)
        {
            // Primary Key.
            builder.HasKey(c => c.Id);

            // Properties.
            builder.Property(c => c.CreationDate)
                .IsRequired();

            builder.Property(c => c.UpdateDate);

            builder.Property(c => c.Cnpj)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.RazaoSocial)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.NomeFantasia)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.InscricaoMunicipal)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.InscricaoEstadual)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(c => c.ContratoSocialBase64)
                .HasColumnType("varchar(MAX)");

            builder.Property(c => c.UltimaAlteracaoBase64)
                .HasColumnType("varchar(MAX)");

            // Table & Column Mappings.
            builder.ToTable("Consultores");

            builder.HasOne(c => c.Onboarding)
                .WithOne(o => o.Consultor)
                .HasForeignKey<Consultor>(c => c.OnboardingId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}