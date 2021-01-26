using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vileve.Infra.Data.Migrations
{
    public partial class Add_TableClientesDependentes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientesDependentes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: true),
                    CodigoParentesco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    NomeCompleto = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    DataNascimento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    TelefoneCelular = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Cep = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Logradouro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Numero = table.Column<int>(nullable: false),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Cidade = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    ClienteId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesDependentes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientesDependentes_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesDependentes_ClienteId",
                table: "ClientesDependentes",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesDependentes");
        }
    }
}
