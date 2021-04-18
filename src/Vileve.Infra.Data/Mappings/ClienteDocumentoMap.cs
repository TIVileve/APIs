using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class ClienteDocumentoMap : IEntityTypeConfiguration<ClienteDocumento>
    {
        public void Configure(EntityTypeBuilder<ClienteDocumento> builder)
        {
            // Primary Key.
            builder.HasKey(cd => cd.Id);

            // Properties.
            builder.Property(cd => cd.CreationDate)
                .IsRequired();

            builder.Property(cd => cd.UpdateDate);

            builder.Property(cd => cd.FrenteBase64);

            builder.Property(cd => cd.VersoBase64);

            builder.Property(cd => cd.TipoDocumento);

            // Table & Column Mappings.
            builder.ToTable("ClientesDocumentos");

            builder.HasOne(cd => cd.Cliente)
                .WithMany(c => c.Documentos)
                .HasForeignKey(cd => cd.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}