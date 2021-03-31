using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Add_TableClientesFontesPagadoras : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientesFontesPagadoras",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    InssNumeroBeneficio = table.Column<long>(nullable: true),
                    InssSalario = table.Column<double>(nullable: true),
                    InssEspecie = table.Column<int>(nullable: true),
                    OutrosDiaPagamento = table.Column<int>(nullable: true),
                    ClienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesFontesPagadoras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesFontesPagadoras_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesFontesPagadoras_ClienteId",
                table: "ClientesFontesPagadoras",
                column: "ClienteId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesFontesPagadoras");
        }
    }
}
