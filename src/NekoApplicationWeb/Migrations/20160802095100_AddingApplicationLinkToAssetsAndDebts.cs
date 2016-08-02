using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class AddingApplicationLinkToAssetsAndDebts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "FinancesDebts",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancesDebts_ApplicationId",
                table: "FinancesDebts",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancesDebts_Applications_ApplicationId",
                table: "FinancesDebts",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancesDebts_Applications_ApplicationId",
                table: "FinancesDebts");

            migrationBuilder.DropIndex(
                name: "IX_FinancesDebts_ApplicationId",
                table: "FinancesDebts");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "FinancesDebts");
        }
    }
}
