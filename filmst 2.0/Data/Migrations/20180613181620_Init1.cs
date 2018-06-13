using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace filmst._0.Data.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackName = table.Column<string>(nullable: false),
                    Album = table.Column<string>(nullable: true),
                    AudioChanels = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Bitrate = table.Column<string>(nullable: true),
                    Bits = table.Column<string>(nullable: true),
                    FileSize = table.Column<int>(nullable: false),
                    Genre = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Length = table.Column<string>(nullable: true),
                    RoomName = table.Column<string>(nullable: true),
                    Year = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackName);
                    table.ForeignKey(
                        name: "FK_Tracks_Rooms_RoomName",
                        column: x => x.RoomName,
                        principalTable: "Rooms",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomName",
                table: "AspNetUsers",
                column: "RoomName");

            migrationBuilder.CreateIndex(
                name: "IX_Tracks_RoomName",
                table: "Tracks",
                column: "RoomName");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomName",
                table: "AspNetUsers",
                column: "RoomName",
                principalTable: "Rooms",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Rooms_RoomName",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomName",
                table: "AspNetUsers");
        }
    }
}
