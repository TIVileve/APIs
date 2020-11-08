using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VilevePay.Domain.Models;

namespace VilevePay.Infra.Data.Mappings
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

            // Table & Column Mappings.
            builder.ToTable("Onboarding");
        }
    }
}