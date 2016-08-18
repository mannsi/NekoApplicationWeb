using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class MissingPropertyValuationColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyValuations",
                table: "PropertyValuations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PropertyValuations");

            migrationBuilder.DropColumn(
                name: "TimeOfData",
                table: "PropertyValuations");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyNumber",
                table: "PropertyValuations",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyValuations",
                table: "PropertyValuations",
                column: "PropertyNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyValuations",
                table: "PropertyValuations");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "PropertyValuations",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeOfData",
                table: "PropertyValuations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "PropertyNumber",
                table: "PropertyValuations",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyValuations",
                table: "PropertyValuations",
                column: "Id");
        }
    }
}
