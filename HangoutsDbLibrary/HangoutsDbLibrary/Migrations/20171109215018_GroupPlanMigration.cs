using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class GroupPlanMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GroupID",
                table: "Plan",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Plan_GroupID",
                table: "Plan",
                column: "GroupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Group_GroupID",
                table: "Plan",
                column: "GroupID",
                principalTable: "Group",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Group_GroupID",
                table: "Plan");

            migrationBuilder.DropIndex(
                name: "IX_Plan_GroupID",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "GroupID",
                table: "Plan");
        }
    }
}
