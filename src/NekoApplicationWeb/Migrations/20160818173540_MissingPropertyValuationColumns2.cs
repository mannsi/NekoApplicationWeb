using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class MissingPropertyValuationColumns2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewFireInsuranceValuation",
                table: "PropertyValuations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PlotAssessmentValue",
                table: "PropertyValuations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateValuation2016",
                table: "PropertyValuations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RealEstateValuation2017",
                table: "PropertyValuations",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NewFireInsuranceValuation",
                table: "PropertyValuations");

            migrationBuilder.DropColumn(
                name: "PlotAssessmentValue",
                table: "PropertyValuations");

            migrationBuilder.DropColumn(
                name: "RealEstateValuation2016",
                table: "PropertyValuations");

            migrationBuilder.DropColumn(
                name: "RealEstateValuation2017",
                table: "PropertyValuations");
        }
    }
}
