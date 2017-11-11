using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class UserFriends : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_UserID",
                table: "User",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_User_UserID",
                table: "User",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_User_UserID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_UserID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "User");
        }
    }
}
