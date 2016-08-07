using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class AddTotalNumbersToApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalAssetAmountForAllApplicants",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalDebtAmountForAllApplicants",
                table: "Applications",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TotalMonthlyIncomeForAllApplicant",
                table: "Applications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAssetAmountForAllApplicants",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "TotalDebtAmountForAllApplicants",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "TotalMonthlyIncomeForAllApplicant",
                table: "Applications");
        }
    }
}
