using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class OnetoManyPlanActivity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ActivityID",
                table: "Plan",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plan_ActivityID",
                table: "Plan",
                column: "ActivityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Plan_Activity_ActivityID",
                table: "Plan",
                column: "ActivityID",
                principalTable: "Activity",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plan_Activity_ActivityID",
                table: "Plan");

            migrationBuilder.DropIndex(
                name: "IX_Plan_ActivityID",
                table: "Plan");

            migrationBuilder.DropColumn(
                name: "ActivityID",
                table: "Plan");
        }
    }
}
