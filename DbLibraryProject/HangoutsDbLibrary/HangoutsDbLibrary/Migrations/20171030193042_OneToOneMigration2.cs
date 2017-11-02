using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HangoutsDbLibrary.Migrations
{
    public partial class OneToOneMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_GroupAdmin_AdminId",
                table: "Groups");

            migrationBuilder.DropIndex(
                name: "IX_Groups_AdminId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "GroupAdminForeignKey",
                table: "GroupAdmin",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupAdmin_GroupAdminForeignKey",
                table: "GroupAdmin",
                column: "GroupAdminForeignKey",
                unique: true,
                filter: "[GroupAdminForeignKey] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupAdmin_Groups_GroupAdminForeignKey",
                table: "GroupAdmin",
                column: "GroupAdminForeignKey",
                principalTable: "Groups",
                principalColumn: "GroupNr",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupAdmin_Groups_GroupAdminForeignKey",
                table: "GroupAdmin");

            migrationBuilder.DropIndex(
                name: "IX_GroupAdmin_GroupAdminForeignKey",
                table: "GroupAdmin");

            migrationBuilder.DropColumn(
                name: "GroupAdminForeignKey",
                table: "GroupAdmin");

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Groups",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Groups_AdminId",
                table: "Groups",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_GroupAdmin_AdminId",
                table: "Groups",
                column: "AdminId",
                principalTable: "GroupAdmin",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
