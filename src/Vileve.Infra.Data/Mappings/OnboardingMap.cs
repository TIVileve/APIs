using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class OnboardingMap : IEntityTypeConfiguration<Onboarding>
    {
        public void Configure(EntityTypeBuilder<Onboarding> builder)
        {
            // Primary Key.
            builder.HasKey(o => o.Id);

            // Properties.
            builder.Property(o => o.CreationDate)
                .IsRequired();

            builder.Property(o => o.UpdateDate);

            builder.Property(o => o.CodigoConvite)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(o => o.NumeroCelular)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(o => o.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(o => o.Senha)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(o => o.StatusOnboarding);

            // Table & Column Mappings.
            builder.ToTable("Onboarding");
        }
    }
}