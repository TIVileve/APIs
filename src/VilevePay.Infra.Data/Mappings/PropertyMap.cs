using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VilevePay.Domain.Models;

namespace VilevePay.Infra.Data.Mappings
{
    public class PropertyMap : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            // Primary Key.
            builder.HasKey(p => p.Id);

            // Properties.
            builder.Property(p => p.CreationDate)
                .IsRequired();

            builder.Property(p => p.UpdateDate);

            builder.Property(p => p.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.Type)
                .IsRequired();

            builder.Property(p => p.IsRequired)
                .IsRequired();

            // Table & Column Mappings.
            builder.ToTable("Properties");
        }
    }
}