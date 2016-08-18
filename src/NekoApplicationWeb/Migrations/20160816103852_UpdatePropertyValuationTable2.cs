using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class UpdatePropertyValuationTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValuations_Applications_ApplicationId",
                table: "PropertyValuations");

            migrationBuilder.DropIndex(
                name: "IX_PropertyValuations_ApplicationId",
                table: "PropertyValuations");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "PropertyValuations");

            migrationBuilder.AddColumn<string>(
                name: "PropertyNumber",
                table: "PropertyValuations",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PropertyNumber",
                table: "PropertyValuations");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "PropertyValuations",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValuations_ApplicationId",
                table: "PropertyValuations",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValuations_Applications_ApplicationId",
                table: "PropertyValuations",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
