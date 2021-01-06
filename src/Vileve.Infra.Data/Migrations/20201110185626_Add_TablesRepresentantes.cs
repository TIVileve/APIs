using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Add_TablesRepresentantes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "RepresentantesEmails");

            migrationBuilder.DropTable(
                name: "RepresentantesTelefones");

            migrationBuilder.DropTable(
                name: "Representantes");
        }
    }
}
