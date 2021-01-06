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
                    Senha = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DadosBancarios");

            migrationBuilder.DropTable(
                name: "Consultores");

            migrationBuilder.DropTable(
                name: "Onboarding");
        }
    }
}
