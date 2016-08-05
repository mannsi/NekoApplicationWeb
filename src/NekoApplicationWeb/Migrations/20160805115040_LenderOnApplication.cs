using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class LenderOnApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoanDetails");

            migrationBuilder.AddColumn<string>(
                name: "LenderId",
                table: "Applications",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applications_LenderId",
                table: "Applications",
                column: "LenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Lenders_LenderId",
                table: "Applications",
                column: "LenderId",
                principalTable: "Lenders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Lenders_LenderId",
                table: "Applications");

            migrationBuilder.DropIndex(
                name: "IX_Applications_LenderId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "LenderId",
                table: "Applications");

            migrationBuilder.CreateTable(
                name: "LoanDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    LenderId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanDetails_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanDetails_Lenders_LenderId",
                        column: x => x.LenderId,
                        principalTable: "Lenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_ApplicationId",
                table: "LoanDetails",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_LenderId",
                table: "LoanDetails",
                column: "LenderId");
        }
    }
}
