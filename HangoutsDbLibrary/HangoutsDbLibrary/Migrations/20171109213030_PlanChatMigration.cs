using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class PlanChatMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plan_ChatID",
                table: "Plan");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_ChatID",
                table: "Plan",
                column: "ChatID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Plan_ChatID",
                table: "Plan");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_ChatID",
                table: "Plan",
                column: "ChatID");
        }
    }
}
