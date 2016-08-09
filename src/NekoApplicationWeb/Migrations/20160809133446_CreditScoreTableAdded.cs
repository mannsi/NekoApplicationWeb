using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class CreditScoreTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CreditScoreEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    AgeGrp = table.Column<int>(nullable: false),
                    CompanyRelation = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PD = table.Column<double>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    RatioBelowAgeGrp = table.Column<float>(nullable: true),
                    RatioBelowAll = table.Column<float>(nullable: true),
                    RatioBelowLocation = table.Column<float>(nullable: true),
                    RegionCode = table.Column<string>(nullable: true),
                    Regno = table.Column<string>(nullable: true),
                    Scoreband = table.Column<string>(nullable: true),
                    Scorestatusdescription = table.Column<string>(nullable: true),
                    ScorestatusdescriptionEN = table.Column<string>(nullable: true),
                    Scorestatusid = table.Column<int>(nullable: false),
                    TimeOfData = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditScoreEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreditScoreEntries");
        }
    }
}
