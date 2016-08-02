using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class TableNameChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantFinancesIncomes");

            migrationBuilder.DropTable(
                name: "FinancesAssets");

            migrationBuilder.DropTable(
                name: "FinancesDebts");

            migrationBuilder.CreateTable(
                name: "ApplicantIncomes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IncomeType = table.Column<int>(nullable: false),
                    MonthlyAmount = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantIncomes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    AssetNumber = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetWillBeSold = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    DebtType = table.Column<int>(nullable: false),
                    Lender = table.Column<string>(nullable: true),
                    LoanRemains = table.Column<int>(nullable: false),
                    MonthlyPayment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Debts_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantIncomes_UserId",
                table: "ApplicantIncomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ApplicationId",
                table: "Assets",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_ApplicationId",
                table: "Debts",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicantIncomes");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.CreateTable(
                name: "ApplicantFinancesIncomes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    IncomeType = table.Column<int>(nullable: false),
                    MonthlyAmount = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantFinancesIncomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantFinancesIncomes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancesAssets",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    AssetNumber = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    AssetTypeString = table.Column<string>(nullable: true),
                    AssetWillBeSold = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancesAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancesAssets_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancesDebts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    DebtType = table.Column<int>(nullable: false),
                    Lender = table.Column<string>(nullable: true),
                    LoanRemains = table.Column<int>(nullable: false),
                    MonthlyPayment = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancesDebts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancesDebts_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantFinancesIncomes_UserId",
                table: "ApplicantFinancesIncomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancesAssets_ApplicationId",
                table: "FinancesAssets",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancesDebts_ApplicationId",
                table: "FinancesDebts",
                column: "ApplicationId");
        }
    }
}
