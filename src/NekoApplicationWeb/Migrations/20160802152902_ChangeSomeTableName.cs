using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NekoApplicationWeb.Migrations
{
    public partial class ChangeSomeTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationEducations_AspNetUsers_UserId",
                table: "ApplicationEducations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationEducations",
                table: "ApplicationEducations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicantEducations",
                table: "ApplicationEducations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicantEducations_AspNetUsers_UserId",
                table: "ApplicationEducations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationEducations_UserId",
                table: "ApplicationEducations",
                newName: "IX_ApplicantEducations_UserId");

            migrationBuilder.RenameTable(
                name: "ApplicationEducations",
                newName: "ApplicantEducations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicantEducations_AspNetUsers_UserId",
                table: "ApplicantEducations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicantEducations",
                table: "ApplicantEducations");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationEducations",
                table: "ApplicantEducations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationEducations_AspNetUsers_UserId",
                table: "ApplicantEducations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameIndex(
                name: "IX_ApplicantEducations_UserId",
                table: "ApplicantEducations",
                newName: "IX_ApplicationEducations_UserId");

            migrationBuilder.RenameTable(
                name: "ApplicantEducations",
                newName: "ApplicationEducations");
        }
    }
}
