using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class MergingDebtsAndAssets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantFinancesAssets");

            migrationBuilder.DropTable(
                name: "ApplicantFinancesDebts");

            migrationBuilder.CreateTable(
                name: "FinancesAssets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssetNumber = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetTypeString = table.Column<string>(nullable: true),
                    AssetWillBeSold = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancesAssets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinancesDebts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DebtType = table.Column<int>(nullable: false),
                    Lender = table.Column<string>(nullable: true),
                    LoanRemains = table.Column<int>(nullable: false),
                    MonthlyPayment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancesDebts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancesAssets");

            migrationBuilder.DropTable(
                name: "FinancesDebts");

            migrationBuilder.CreateTable(
                name: "ApplicantFinancesAssets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AssetNumber = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetTypeString = table.Column<string>(nullable: true),
                    AssetWillBeSold = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantFinancesAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantFinancesAssets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantFinancesDebts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DebtType = table.Column<int>(nullable: false),
                    Lender = table.Column<string>(nullable: true),
                    LoanRemains = table.Column<int>(nullable: false),
                    MonthlyPayment = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantFinancesDebts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantFinancesDebts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantFinancesAssets_UserId",
                table: "ApplicantFinancesAssets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantFinancesDebts_UserId",
                table: "ApplicantFinancesDebts",
                column: "UserId");
        }
    }
}
