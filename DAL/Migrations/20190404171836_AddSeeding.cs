using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class AddSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { 1L, "461ae62e-86c8-4930-9fc1-740930ceb4ff", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1L, 0, "92183c3f-a5af-463f-bf2e-77e0183469e7", null, false, false, null, null, "ADMIN", null, null, false, null, false, "admin" });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "DateSent", "HashMessage", "RoomId", "UserId" },
                values: new object[] { 1L, new DateTime(2019, 4, 4, 17, 18, 35, 703, DateTimeKind.Utc).AddTicks(3838), "SomeMessage", 1L, 1L });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "HostId", "Name", "PasswordHash", "PlayListId", "UniqName" },
                values: new object[] { 1L, 0L, "Room1", null, 0L, "UniqRoomNameAzaza" });

            migrationBuilder.InsertData(
                table: "PlayLists",
                columns: new[] { "Id", "RoomId", "TrackCurrentTime" },
                values: new object[] { 1L, 1L, new TimeSpan(0, 0, 0, 0, 0) });

            migrationBuilder.InsertData(
                table: "UserRoom",
                columns: new[] { "RoomId", "UserId" },
                values: new object[] { 1L, 1L });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Messages",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "PlayLists",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserRoom",
                keyColumns: new[] { "RoomId", "UserId" },
                keyValues: new object[] { 1L, 1L });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Id",
                keyValue: 1L);
        }
    }
}
