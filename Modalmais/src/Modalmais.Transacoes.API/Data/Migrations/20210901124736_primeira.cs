using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Modalmais.Transacoes.API.Data.Migrations
{
    public partial class primeira : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "modalmais");

            migrationBuilder.CreateTable(
                name: "Transacoes",
                schema: "modalmais",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 36, nullable: false),
                    StatusTransacao = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    Chave = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(6,2)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(50)", maxLength: 30, nullable: true),
                    Conta_Banco = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    Conta_Agencia = table.Column<string>(type: "varchar(4)", maxLength: 4, nullable: true),
                    Conta_Numero = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.CheckConstraint("CK_Transacoes_StatusTransacao_Enum", "\"StatusTransacao\" IN (0, 1, 2)");
                    table.CheckConstraint("CK_Transacoes_Valor", "\"Valor\" >= 0.01 AND \"Valor\" <= 5000.00");
                    table.CheckConstraint("CK_Transacoes_Tipo_Enum", "\"Tipo\" IN (1, 2, 3, 4)");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacoes",
                schema: "modalmais");
        }
    }
}
