using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class AddingApplicationLinkToAssetsAndDebts2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "FinancesAssets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancesAssets_ApplicationId",
                table: "FinancesAssets",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancesAssets_Applications_ApplicationId",
                table: "FinancesAssets",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancesAssets_Applications_ApplicationId",
                table: "FinancesAssets");

            migrationBuilder.DropIndex(
                name: "IX_FinancesAssets_ApplicationId",
                table: "FinancesAssets");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "FinancesAssets");
        }
    }
}
