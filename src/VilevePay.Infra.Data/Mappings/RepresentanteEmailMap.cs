using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VilevePay.Domain.Models;

namespace VilevePay.Infra.Data.Mappings
{
    public class RepresentanteEmailMap : IEntityTypeConfiguration<RepresentanteEmail>
    {
        public void Configure(EntityTypeBuilder<RepresentanteEmail> builder)
        {
            // Primary Key.
            builder.HasKey(re => re.Id);

            // Properties.
            builder.Property(re => re.CreationDate)
                .IsRequired();

            builder.Property(re => re.UpdateDate);

            builder.Property(re => re.TipoEmail);

            builder.Property(re => re.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            // Table & Column Mappings.
            builder.ToTable("RepresentantesEmails");

            builder.HasOne(re => re.Representante)
                .WithMany(r => r.Emails)
                .HasForeignKey(re => re.RepresentanteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}