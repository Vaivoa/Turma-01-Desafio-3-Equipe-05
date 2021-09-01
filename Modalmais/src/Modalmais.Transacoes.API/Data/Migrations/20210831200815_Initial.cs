using Microsoft.EntityFrameworkCore.Migrations;

namespace Modalmais.Transacoes.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Transacoes_StatusTransacao_Enum",
                schema: "modalmais",
                table: "Transacoes");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Transacoes_Valor",
                schema: "modalmais",
                table: "Transacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "modalmais",
                table: "Transacoes",
                type: "varchar(100)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Chave",
                schema: "modalmais",
                table: "Transacoes",
                type: "varchar(100)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 32);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transacoes_StatusTransacao_Enum",
                schema: "modalmais",
                table: "Transacoes",
                sql: "\"StatusTransacao\" IN (0, 1, 2)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Transacoes_StatusTransacao_Enum",
                schema: "modalmais",
                table: "Transacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                schema: "modalmais",
                table: "Transacoes",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Chave",
                schema: "modalmais",
                table: "Transacoes",
                type: "character varying(50)",
                maxLength: 32,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 50);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transacoes_StatusTransacao_Enum",
                schema: "modalmais",
                table: "Transacoes",
                sql: "\"StatusTransacao\" IN (0, 1)");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Transacoes_Valor",
                schema: "modalmais",
                table: "Transacoes",
                sql: "\"Valor\" >= 0.01 AND \"Valor\" <= 5000.00");
        }
    }
}
