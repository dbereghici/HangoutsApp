using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class OneToManyCascadeOnDeleteMigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity",
                column: "GroupNr",
                principalTable: "Groups",
                principalColumn: "GroupNr",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_Groups_GroupNr",
                table: "Activity",
                column: "GroupNr",
                principalTable: "Groups",
                principalColumn: "GroupNr",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
