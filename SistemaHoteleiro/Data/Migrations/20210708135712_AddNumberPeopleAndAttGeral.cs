using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaHoteleiro.Data.Migrations
{
    public partial class AddNumberPeopleAndAttGeral : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberPeople",
                table: "Reserves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<long>(
                name: "Cpf",
                table: "Guests",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberPeople",
                table: "Reserves");

            migrationBuilder.AlterColumn<string>(
                name: "Cpf",
                table: "Guests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");
        }
    }
}
