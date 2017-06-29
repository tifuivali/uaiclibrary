using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedfacultyModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Professors",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacultyDao",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    Name = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyDao", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_FacultyId",
                table: "Professors",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_FacultyDao_FacultyId",
                table: "Professors",
                column: "FacultyId",
                principalTable: "FacultyDao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_FacultyDao_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "FacultyDao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_FacultyDao_FacultyId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_FacultyDao_FacultyId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "FacultyDao");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Professors_FacultyId",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Professors");
        }
    }
}
