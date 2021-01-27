using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Add_TableHotfix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_ConsultorId",
                table: "Clientes");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ConsultorId",
                table: "Clientes",
                column: "ConsultorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Clientes_ConsultorId",
                table: "Clientes");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ConsultorId",
                table: "Clientes",
                column: "ConsultorId",
                unique: true,
                filter: "[ConsultorId] IS NOT NULL");
        }
    }
}
