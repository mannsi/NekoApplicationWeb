using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class DbVersion1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserDisplayName",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "ApplicationEducations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Degree = table.Column<string>(nullable: true),
                    FinishingDate = table.Column<DateTime>(nullable: false),
                    School = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationEducations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationEducations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicantEmployments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    StartingTime = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicantEmployments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicantEmployments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                name: "Application",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    TimeCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Application_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterestsEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FixedInterestsYears = table.Column<int>(nullable: false),
                    Indexed = table.Column<bool>(nullable: false),
                    InterestPercentage = table.Column<double>(nullable: false),
                    InterestsForm = table.Column<int>(nullable: false),
                    LenderId = table.Column<string>(nullable: true),
                    LoanPaymentType = table.Column<int>(nullable: false),
                    LoanTimeYearsMax = table.Column<int>(nullable: false),
                    LoanTimeYearsMin = table.Column<int>(nullable: false),
                    LoanToValueEndPercentage = table.Column<int>(nullable: false),
                    LoanToValueStartPercentage = table.Column<int>(nullable: false),
                    LoanType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestsEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterestsEntries_Lenders_LenderId",
                        column: x => x.LenderId,
                        principalTable: "Lenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThjodskraFamilyEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FamilyNumber = table.Column<string>(nullable: true),
                    GenderCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Ssn = table.Column<string>(nullable: true),
                    TimeOfData = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThjodskraFamilyEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ThjodskraPersons",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FamilyNumber = table.Column<string>(nullable: true),
                    GenderCode = table.Column<int>(nullable: false),
                    Home = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PlaceOfResidence = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    Ssn = table.Column<string>(nullable: true),
                    TimeOfData = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThjodskraPersons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThjodskraPersons_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserConnections",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    UserHasAgreedToEula = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserConnections_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationUserConnections_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

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
                        name: "FK_LoanDetails_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoanDetails_Lenders_LenderId",
                        column: x => x.LenderId,
                        principalTable: "Lenders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyDetails",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    BuyingPrice = table.Column<int>(nullable: false),
                    OwnCapital = table.Column<int>(nullable: false),
                    PropertyNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyDetails_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValuations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    TimeOfData = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValuations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyValuations_Application_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Application",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.AlterColumn<int>(
                name: "LoanPaymentServiceFee",
                table: "Lenders",
                nullable: false,
                defaultValue: 130);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationEducations_UserId",
                table: "ApplicationEducations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantEmployments_UserId",
                table: "ApplicantEmployments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantFinancesAssets_UserId",
                table: "ApplicantFinancesAssets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantFinancesDebts_UserId",
                table: "ApplicantFinancesDebts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicantFinancesIncomes_UserId",
                table: "ApplicantFinancesIncomes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Application_CreatedByUserId",
                table: "Application",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserConnections_ApplicationId",
                table: "ApplicationUserConnections",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserConnections_UserId",
                table: "ApplicationUserConnections",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_ApplicationId",
                table: "LoanDetails",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanDetails_LenderId",
                table: "LoanDetails",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_InterestsEntries_LenderId",
                table: "InterestsEntries",
                column: "LenderId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyDetails_ApplicationId",
                table: "PropertyDetails",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyValuations_ApplicationId",
                table: "PropertyValuations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_ThjodskraPersons_UserId",
                table: "ThjodskraPersons",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationEducations");

            migrationBuilder.DropTable(
                name: "ApplicantEmployments");

            migrationBuilder.DropTable(
                name: "ApplicantFinancesAssets");

            migrationBuilder.DropTable(
                name: "ApplicantFinancesDebts");

            migrationBuilder.DropTable(
                name: "ApplicantFinancesIncomes");

            migrationBuilder.DropTable(
                name: "ApplicationUserConnections");

            migrationBuilder.DropTable(
                name: "LoanDetails");

            migrationBuilder.DropTable(
                name: "InterestsEntries");

            migrationBuilder.DropTable(
                name: "PropertyDetails");

            migrationBuilder.DropTable(
                name: "PropertyValuations");

            migrationBuilder.DropTable(
                name: "ThjodskraFamilyEntries");

            migrationBuilder.DropTable(
                name: "ThjodskraPersons");

            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.AddColumn<string>(
                name: "UserDisplayName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LoanPaymentServiceFee",
                table: "Lenders",
                nullable: false);
        }
    }
}
