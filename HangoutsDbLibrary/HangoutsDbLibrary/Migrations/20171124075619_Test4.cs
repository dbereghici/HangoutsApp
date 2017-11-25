using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class Test4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_User_UserID",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_UserID",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Group");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserID",
                table: "Group",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Group_UserID",
                table: "Group",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Group_User_UserID",
                table: "Group",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
