using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Incognito.Migrations
{
    public partial class ṂodifyAnnotationToReportMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReportMessages_ApplicationUser_UserId",
                table: "ReportMessages");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_ReportMessages_UserId",
                table: "ReportMessages");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReportMessages",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ReportMessages",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "ReportMessages",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Reason",
                table: "ReportMessages",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 500);

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    SecurityStamp = table.Column<string>(nullable: true),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReportMessages_UserId",
                table: "ReportMessages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReportMessages_ApplicationUser_UserId",
                table: "ReportMessages",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
