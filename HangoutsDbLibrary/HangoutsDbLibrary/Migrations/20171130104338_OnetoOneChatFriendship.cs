using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class OnetoOneChatFriendship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.AddColumn<int>(
                name: "ChatID",
                table: "Friendship",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Chat",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Friendship_ChatID",
                table: "Friendship",
                column: "ChatID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_Chat_ChatID",
                table: "Friendship",
                column: "ChatID",
                principalTable: "Chat",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_Chat_ChatID",
                table: "Friendship");

            migrationBuilder.DropIndex(
                name: "IX_Friendship_ChatID",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "ChatID",
                table: "Friendship");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Activity");

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Password = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.ID);
                });
        }
    }
}
