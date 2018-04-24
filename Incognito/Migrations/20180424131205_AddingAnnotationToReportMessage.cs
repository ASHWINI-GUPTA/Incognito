using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Incognito.Migrations
{
    public partial class AddingAnnotationToReportMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportMessages_ApplicationUser_UserId",
                table: "ReportMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReportMessages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ReportMessages",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReportMessages_ApplicationUser_UserId",
                table: "ReportMessages",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportMessages_ApplicationUser_UserId",
                table: "ReportMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReportMessages",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ReportMessages",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_ReportMessages_ApplicationUser_UserId",
                table: "ReportMessages",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
