using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedBookFacultyRelationShip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_FacultyId",
                table: "Books",
                column: "FacultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Faculties_FacultyId",
                table: "Books",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Faculties_FacultyId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_FacultyId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FacultyId",
                table: "Books");
        }
    }
}
