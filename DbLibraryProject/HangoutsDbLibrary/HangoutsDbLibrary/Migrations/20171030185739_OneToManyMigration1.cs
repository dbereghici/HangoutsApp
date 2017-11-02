using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class OneToManyMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GroupNr",
                table: "Activity",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activity_GroupNr",
                table: "Activity",
                column: "GroupNr");

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

            migrationBuilder.DropIndex(
                name: "IX_Activity_GroupNr",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "GroupNr",
                table: "Activity");
        }
    }
}
