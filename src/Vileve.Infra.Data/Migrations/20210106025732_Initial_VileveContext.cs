using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Initial_VileveContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Onboarding",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    CodigoConvite = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NumeroCelular = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Senha = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    StatusOnboarding = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onboarding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Consultores",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Cnpj = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    RazaoSocial = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NomeFantasia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    InscricaoMunicipal = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    InscricaoEstadual = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ContratoSocialBase64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    UltimaAlteracaoBase64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    OnboardingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultores_Onboarding_OnboardingId",
                        column: x => x.OnboardingId,
                        principalTable: "Onboarding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DadosBancarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    CodigoBanco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Agencia = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ContaSemDigito = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Digito = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TipoConta = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ConsultorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DadosBancarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DadosBancarios_Consultores_ConsultorId",
                        column: x => x.ConsultorId,
                        principalTable: "Consultores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    TipoEndereco = table.Column<int>(nullable: false),
                    Cep = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Principal = table.Column<bool>(nullable: false),
                    ComprovanteBase64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ConsultorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enderecos_Consultores_ConsultorId",
                        column: x => x.ConsultorId,
                        principalTable: "Consultores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Representantes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Cpf = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NomeCompleto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Sexo = table.Column<int>(nullable: false),
                    EstadoCivil = table.Column<int>(nullable: false),
                    Nacionalidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    DocumentoFrenteBase64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DocumentoVersoBase64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ConsultorId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Representantes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Representantes_Consultores_ConsultorId",
                        column: x => x.ConsultorId,
                        principalTable: "Consultores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepresentantesEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    TipoEmail = table.Column<int>(nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    RepresentanteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentantesEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepresentantesEmails_Representantes_RepresentanteId",
                        column: x => x.RepresentanteId,
                        principalTable: "Representantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RepresentantesTelefones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    TipoTelefone = table.Column<int>(nullable: false),
                    Numero = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    RepresentanteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepresentantesTelefones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RepresentantesTelefones_Representantes_RepresentanteId",
                        column: x => x.RepresentanteId,
                        principalTable: "Representantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultores_OnboardingId",
                table: "Consultores",
                column: "OnboardingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DadosBancarios_ConsultorId",
                table: "DadosBancarios",
                column: "ConsultorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_ConsultorId",
                table: "Enderecos",
                column: "ConsultorId");

            migrationBuilder.CreateIndex(
                name: "IX_Representantes_ConsultorId",
                table: "Representantes",
                column: "ConsultorId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepresentantesEmails_RepresentanteId",
                table: "RepresentantesEmails",
                column: "RepresentanteId");

            migrationBuilder.CreateIndex(
                name: "IX_RepresentantesTelefones_RepresentanteId",
                table: "RepresentantesTelefones",
                column: "RepresentanteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosBancarios");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "RepresentantesEmails");

            migrationBuilder.DropTable(
                name: "RepresentantesTelefones");

            migrationBuilder.DropTable(
                name: "Representantes");

            migrationBuilder.DropTable(
                name: "Consultores");

            migrationBuilder.DropTable(
                name: "Onboarding");
        }
    }
}
