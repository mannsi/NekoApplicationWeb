using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class SpouseSsnInThjodskra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Application_AspNetUsers_CreatedByUserId",
                table: "Application");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserConnections_Application_ApplicationId",
                table: "ApplicationUserConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDetails_Application_ApplicationId",
                table: "LoanDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Application_ApplicationId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValuations_Application_ApplicationId",
                table: "PropertyValuations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Application",
                table: "Application");

            migrationBuilder.AddColumn<string>(
                name: "SpouseSsn",
                table: "ThjodskraPersons",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeletable",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applications",
                table: "Application",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_AspNetUsers_CreatedByUserId",
                table: "Application",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserConnections_Applications_ApplicationId",
                table: "ApplicationUserConnections",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDetails_Applications_ApplicationId",
                table: "LoanDetails",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Applications_ApplicationId",
                table: "PropertyDetails",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValuations_Applications_ApplicationId",
                table: "PropertyValuations",
                column: "ApplicationId",
                principalTable: "Application",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Application_CreatedByUserId",
                table: "Application",
                newName: "IX_Applications_CreatedByUserId");

            migrationBuilder.RenameTable(
                name: "Application",
                newName: "Applications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_AspNetUsers_CreatedByUserId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserConnections_Applications_ApplicationId",
                table: "ApplicationUserConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanDetails_Applications_ApplicationId",
                table: "LoanDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyDetails_Applications_ApplicationId",
                table: "PropertyDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyValuations_Applications_ApplicationId",
                table: "PropertyValuations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applications",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "SpouseSsn",
                table: "ThjodskraPersons");

            migrationBuilder.DropColumn(
                name: "IsDeletable",
                table: "AspNetUsers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Application",
                table: "Applications",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Application_AspNetUsers_CreatedByUserId",
                table: "Applications",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserConnections_Application_ApplicationId",
                table: "ApplicationUserConnections",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanDetails_Application_ApplicationId",
                table: "LoanDetails",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyDetails_Application_ApplicationId",
                table: "PropertyDetails",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyValuations_Application_ApplicationId",
                table: "PropertyValuations",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_Applications_CreatedByUserId",
                table: "Applications",
                newName: "IX_Application_CreatedByUserId");

            migrationBuilder.RenameTable(
                name: "Applications",
                newName: "Application");
        }
    }
}
