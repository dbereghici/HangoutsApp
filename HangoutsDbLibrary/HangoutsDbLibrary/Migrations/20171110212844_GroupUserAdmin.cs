using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class GroupUserAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Group_GroupAdministratedID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_GroupAdministratedID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "GroupAdministratedID",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "AdminID",
                table: "Group",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Group_AdminID",
                table: "Group",
                column: "AdminID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Group_User_AdminID",
                table: "Group",
                column: "AdminID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Group_User_AdminID",
                table: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Group_AdminID",
                table: "Group");

            migrationBuilder.DropColumn(
                name: "AdminID",
                table: "Group");

            migrationBuilder.AddColumn<int>(
                name: "GroupAdministratedID",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_User_GroupAdministratedID",
                table: "User",
                column: "GroupAdministratedID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Group_GroupAdministratedID",
                table: "User",
                column: "GroupAdministratedID",
                principalTable: "Group",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
