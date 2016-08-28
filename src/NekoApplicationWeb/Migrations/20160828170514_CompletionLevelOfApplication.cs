using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class CompletionLevelOfApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EducationPageCompleted",
                table: "Applications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EmploymentPageCompleted",
                table: "Applications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FinancesPageCompleted",
                table: "Applications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LoanPageCompleted",
                table: "Applications",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PersonalPageCompleted",
                table: "Applications",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EducationPageCompleted",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "EmploymentPageCompleted",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "FinancesPageCompleted",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LoanPageCompleted",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "PersonalPageCompleted",
                table: "Applications");
        }
    }
}
