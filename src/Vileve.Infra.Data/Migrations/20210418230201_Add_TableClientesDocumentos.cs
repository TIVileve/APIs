using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Add_TableClientesDocumentos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientesDocumentos",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    FrenteBase64 = table.Column<string>(nullable: true),
                    VersoBase64 = table.Column<string>(nullable: true),
                    TipoDocumento = table.Column<int>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesDocumentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesDocumentos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesDocumentos_ClienteId",
                table: "ClientesDocumentos",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesDocumentos");
        }
    }
}
