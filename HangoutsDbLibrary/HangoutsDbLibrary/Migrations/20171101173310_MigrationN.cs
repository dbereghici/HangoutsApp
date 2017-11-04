using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class MigrationN : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity");

            migrationBuilder.AddColumn<string>(
                name: "activity_type",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SportName",
                table: "Activity",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserGroup",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGroup", x => new { x.UserId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_UserGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupNr",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserGroup_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserGroup_GroupId",
                table: "UserGroup",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity",
                column: "GroupNr",
                principalTable: "Groups",
                principalColumn: "GroupNr",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity");

            migrationBuilder.DropTable(
                name: "UserGroup");

            migrationBuilder.DropColumn(
                name: "activity_type",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "SportName",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity",
                column: "GroupNr",
                principalTable: "Groups",
                principalColumn: "GroupNr",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
