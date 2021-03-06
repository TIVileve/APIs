﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Vileve.Infra.Data.Context;

namespace Vileve.Infra.Data.Migrations
{
    [DbContext(typeof(VileveContext))]
    partial class VileveContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Vileve.Domain.Models.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ConsultorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomeCompleto")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("TelefoneCelular")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("TelefoneFixo")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConsultorId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteDependente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodigoParentesco")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomeCompleto")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<string>("TelefoneCelular")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("ClientesDependentes");
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteDocumento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("FrenteBase64")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TipoDocumento")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("VersoBase64")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("ClientesDocumentos");
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteEndereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ComprovanteBase64")
                        .HasColumnType("varchar(MAX)");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("ClientesEnderecos");
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteFontePagadora", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<int?>("InssEspecie")
                        .HasColumnType("int");

                    b.Property<long?>("InssNumeroBeneficio")
                        .HasColumnType("bigint");

                    b.Property<double?>("InssSalario")
                        .HasColumnType("float");

                    b.Property<int?>("OutrosDiaPagamento")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("ClientesFontesPagadoras");
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteProduto", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodigoProdutoItem")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId")
                        .IsUnique();

                    b.ToTable("ClientesProdutos");
                });

            modelBuilder.Entity("Vileve.Domain.Models.Consultor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cnpj")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ContratoSocialBase64")
                        .HasColumnType("varchar(MAX)");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("InscricaoEstadual")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("InscricaoMunicipal")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("OnboardingId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RazaoSocial")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("UltimaAlteracaoBase64")
                        .HasColumnType("varchar(MAX)");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("OnboardingId")
                        .IsUnique();

                    b.ToTable("Consultores");
                });

            modelBuilder.Entity("Vileve.Domain.Models.DadosBancarios", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Agencia")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("CodigoBanco")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("ConsultorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContaSemDigito")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Digito")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("TipoConta")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConsultorId")
                        .IsUnique();

                    b.ToTable("DadosBancarios");
                });

            modelBuilder.Entity("Vileve.Domain.Models.Endereco", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Bairro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cep")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cidade")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("ComprovanteBase64")
                        .HasColumnType("varchar(MAX)");

                    b.Property<Guid>("ConsultorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Estado")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Logradouro")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<bool>("Principal")
                        .HasColumnType("bit");

                    b.Property<int>("TipoEndereco")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConsultorId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Vileve.Domain.Models.Onboarding", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CodigoConvite")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NumeroCelular")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Senha")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("StatusOnboarding")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Onboarding");
                });

            modelBuilder.Entity("Vileve.Domain.Models.Representante", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ConsultorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cpf")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("DocumentoFrenteBase64")
                        .HasColumnType("varchar(MAX)");

                    b.Property<string>("DocumentoVersoBase64")
                        .HasColumnType("varchar(MAX)");

                    b.Property<int>("EstadoCivil")
                        .HasColumnType("int");

                    b.Property<string>("Nacionalidade")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("NomeCompleto")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ConsultorId")
                        .IsUnique();

                    b.ToTable("Representantes");
                });

            modelBuilder.Entity("Vileve.Domain.Models.RepresentanteEmail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("RepresentanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoEmail")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RepresentanteId");

                    b.ToTable("RepresentantesEmails");
                });

            modelBuilder.Entity("Vileve.Domain.Models.RepresentanteTelefone", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreationDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Numero")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<Guid>("RepresentanteId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("TipoTelefone")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("RepresentanteId");

                    b.ToTable("RepresentantesTelefones");
                });

            modelBuilder.Entity("Vileve.Domain.Models.Cliente", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Consultor", "Consultor")
                        .WithMany()
                        .HasForeignKey("ConsultorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteDependente", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Cliente", "Cliente")
                        .WithMany("Dependentes")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteDocumento", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Cliente", "Cliente")
                        .WithMany("Documentos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteEndereco", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Cliente", "Cliente")
                        .WithMany("Enderecos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteFontePagadora", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Cliente", "Cliente")
                        .WithOne("FontePagadora")
                        .HasForeignKey("Vileve.Domain.Models.ClienteFontePagadora", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.ClienteProduto", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Cliente", "Cliente")
                        .WithOne("Produto")
                        .HasForeignKey("Vileve.Domain.Models.ClienteProduto", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.Consultor", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Onboarding", "Onboarding")
                        .WithOne("Consultor")
                        .HasForeignKey("Vileve.Domain.Models.Consultor", "OnboardingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.DadosBancarios", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Consultor", "Consultor")
                        .WithOne("DadosBancarios")
                        .HasForeignKey("Vileve.Domain.Models.DadosBancarios", "ConsultorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.Endereco", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Consultor", "Consultor")
                        .WithMany("Enderecos")
                        .HasForeignKey("ConsultorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.Representante", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Consultor", "Consultor")
                        .WithOne("Representante")
                        .HasForeignKey("Vileve.Domain.Models.Representante", "ConsultorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.RepresentanteEmail", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Representante", "Representante")
                        .WithMany("Emails")
                        .HasForeignKey("RepresentanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Vileve.Domain.Models.RepresentanteTelefone", b =>
                {
                    b.HasOne("Vileve.Domain.Models.Representante", "Representante")
                        .WithMany("Telefones")
                        .HasForeignKey("RepresentanteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
