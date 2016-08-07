using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class RemoveApplicationFromCostOfLiving : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CostOfLivingEntries_Applications_ApplicationId",
                table: "CostOfLivingEntries");

            migrationBuilder.DropIndex(
                name: "IX_CostOfLivingEntries_ApplicationId",
                table: "CostOfLivingEntries");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "CostOfLivingEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "CostOfLivingEntries",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostOfLivingEntries_ApplicationId",
                table: "CostOfLivingEntries",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CostOfLivingEntries_Applications_ApplicationId",
                table: "CostOfLivingEntries",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
