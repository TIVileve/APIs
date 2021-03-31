using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class ClienteFontePagadoraMap : IEntityTypeConfiguration<ClienteFontePagadora>
    {
        public void Configure(EntityTypeBuilder<ClienteFontePagadora> builder)
        {
            // Primary Key.
            builder.HasKey(cfp => cfp.Id);

            // Properties.
            builder.Property(cfp => cfp.CreationDate)
                .IsRequired();

            builder.Property(cfp => cfp.UpdateDate);

            builder.Property(cfp => cfp.InssNumeroBeneficio);

            builder.Property(cfp => cfp.InssSalario);

            builder.Property(cfp => cfp.InssEspecie);

            builder.Property(cfp => cfp.OutrosDiaPagamento);

            // Table & Column Mappings.
            builder.ToTable("ClientesFontesPagadoras");

            builder.HasOne(cfp => cfp.Cliente)
                .WithOne(c => c.FontePagadora)
                .HasForeignKey<ClienteFontePagadora>(cfp => cfp.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}