using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class CostOfLivingTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CostOfLivingEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ApplicationId = table.Column<string>(nullable: true),
                    CostOfLivingWithoutTransportationAndHousing = table.Column<int>(nullable: false),
                    NumberOfAdults = table.Column<int>(nullable: false),
                    NumberOfKidsElementarySchool = table.Column<int>(nullable: false),
                    NumberOfKidsInKindergarten = table.Column<int>(nullable: false),
                    TransportationCostIfNoCar = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostOfLivingEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostOfLivingEntries_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CostOfLivingEntries_ApplicationId",
                table: "CostOfLivingEntries",
                column: "ApplicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CostOfLivingEntries");
        }
    }
}
