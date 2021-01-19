using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Add_TableClientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    Cpf = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NomeCompleto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    DataNascimento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TelefoneFixo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TelefoneCelular = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ConsultorId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Consultores_ConsultorId",
                        column: x => x.ConsultorId,
                        principalTable: "Consultores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_ConsultorId",
                table: "Clientes",
                column: "ConsultorId",
                unique: true,
                filter: "[ConsultorId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
