using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaHoteleiro.Data.Migrations
{
    public partial class RemoveGuestId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryRooms_Rooms_RoomId",
                table: "CategoryRooms");

            migrationBuilder.DropIndex(
                name: "IX_CategoryRooms_RoomId",
                table: "CategoryRooms");

            migrationBuilder.DropColumn(
                name: "GuestId",
                table: "CategoryRooms");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "CategoryRooms");

            migrationBuilder.AddColumn<int>(
                name: "CategoryRoomId1",
                table: "Rooms",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_CategoryRoomId1",
                table: "Rooms",
                column: "CategoryRoomId1",
                unique: true,
                filter: "[CategoryRoomId1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_CategoryRooms_CategoryRoomId1",
                table: "Rooms",
                column: "CategoryRoomId1",
                principalTable: "CategoryRooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_CategoryRooms_CategoryRoomId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_CategoryRoomId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CategoryRoomId1",
                table: "Rooms");

            migrationBuilder.AddColumn<int>(
                name: "GuestId",
                table: "CategoryRooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "CategoryRooms",
                type: "int",
                nullable: true);

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
        }
    }
}
