using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Modify_ColumnCodigoProdutoItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoProduto",
                table: "ClientesProdutos",
                newName: "CodigoProdutoItem");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodigoProdutoItem",
                table: "ClientesProdutos",
                newName: "CodigoProduto");
        }
    }
}
