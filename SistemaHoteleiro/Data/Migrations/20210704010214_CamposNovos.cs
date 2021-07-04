using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaHoteleiro.Data.Migrations
{
    public partial class CamposNovos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Reserves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Cpf",
                table: "Reserves",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Reserves",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Fone",
                table: "Reserves",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Fone",
                table: "Guests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Fone",
                table: "Reserves");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "Guests");

            migrationBuilder.DropColumn(
                name: "Fone",
                table: "Guests");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
