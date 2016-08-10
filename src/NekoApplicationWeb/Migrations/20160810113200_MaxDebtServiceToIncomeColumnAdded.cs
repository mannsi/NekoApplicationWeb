using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class MaxDebtServiceToIncomeColumnAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxDebtServiceToIncome",
                table: "Lenders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxDebtServiceToIncome",
                table: "Lenders");
        }
    }
}
