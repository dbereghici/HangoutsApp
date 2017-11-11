using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class FriendshipDeleteBehaviorCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_UserID1",
                table: "Friendship");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_UserID1",
                table: "Friendship",
                column: "UserID1",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendship_User_UserID1",
                table: "Friendship");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendship_User_UserID1",
                table: "Friendship",
                column: "UserID1",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
