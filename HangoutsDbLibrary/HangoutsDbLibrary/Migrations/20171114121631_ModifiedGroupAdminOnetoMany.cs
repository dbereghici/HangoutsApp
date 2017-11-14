using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class ModifiedGroupAdminOnetoMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Group_AdminID",
                table: "Group");

            migrationBuilder.CreateIndex(
                name: "IX_Group_AdminID",
                table: "Group",
                column: "AdminID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Group_AdminID",
                table: "Group");

            migrationBuilder.CreateIndex(
                name: "IX_Group_AdminID",
                table: "Group",
                column: "AdminID",
                unique: true);
        }
    }
}
