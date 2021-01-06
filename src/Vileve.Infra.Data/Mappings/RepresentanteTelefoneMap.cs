using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class RepresentanteTelefoneMap : IEntityTypeConfiguration<RepresentanteTelefone>
    {
        public void Configure(EntityTypeBuilder<RepresentanteTelefone> builder)
        {
            // Primary Key.
            builder.HasKey(rt => rt.Id);

            // Properties.
            builder.Property(rt => rt.CreationDate)
                .IsRequired();

            builder.Property(rt => rt.UpdateDate);

            builder.Property(rt => rt.TipoTelefone);

            builder.Property(rt => rt.Numero)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            // Table & Column Mappings.
            builder.ToTable("RepresentantesTelefones");

            builder.HasOne(rt => rt.Representante)
                .WithMany(r => r.Telefones)
                .HasForeignKey(rt => rt.RepresentanteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}