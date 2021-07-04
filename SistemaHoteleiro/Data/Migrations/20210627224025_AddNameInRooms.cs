using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaHoteleiro.Data.Migrations
{
    public partial class AddNameInRooms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "CategoryRooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CategoryRoomId",
                table: "Rooms",
                column: "CategoryRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_GuestId",
                table: "Reserves",
                column: "GuestId");

            migrationBuilder.CreateIndex(
                name: "IX_Reserves_RoomId",
                table: "Reserves",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRooms_RoomId",
                table: "CategoryRooms",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRooms_Rooms_RoomId",
                table: "CategoryRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Guests_GuestId",
                table: "Reserves",
                column: "GuestId",
                principalTable: "Guests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reserves_Rooms_RoomId",
                table: "Reserves",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_CategoryRooms_CategoryRoomId",
                table: "Rooms",
                column: "CategoryRoomId",
                principalTable: "CategoryRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRooms_Rooms_RoomId",
                table: "CategoryRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Guests_GuestId",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_Reserves_Rooms_RoomId",
                table: "Reserves");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_CategoryRooms_CategoryRoomId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CategoryRoomId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_GuestId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_Reserves_RoomId",
                table: "Reserves");

            migrationBuilder.DropIndex(
                name: "IX_CategoryRooms_RoomId",
                table: "CategoryRooms");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "CategoryRooms");
        }
    }
}
