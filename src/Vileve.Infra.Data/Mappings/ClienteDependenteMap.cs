using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vileve.Domain.Models;

namespace Vileve.Infra.Data.Mappings
{
    public class ClienteDependenteMap : IEntityTypeConfiguration<ClienteDependente>
    {
        public void Configure(EntityTypeBuilder<ClienteDependente> builder)
        {
            // Primary Key.
            builder.HasKey(cd => cd.Id);

            // Properties.
            builder.Property(cd => cd.CreationDate)
                .IsRequired();

            builder.Property(cd => cd.UpdateDate);

            builder.Property(cd => cd.CodigoParentesco)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.NomeCompleto)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.DataNascimento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Cpf)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.TelefoneCelular)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Cep)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Logradouro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Numero);

            builder.Property(cd => cd.Complemento)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Bairro)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Cidade)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(cd => cd.Estado)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            // Table & Column Mappings.
            builder.ToTable("ClientesDependentes");

            builder.HasOne(cd => cd.Cliente)
                .WithMany(c => c.Dependentes)
                .HasForeignKey(cd => cd.ClienteId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}