using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedFacultyTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_FacultyDao_FacultyId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_FacultyDao_FacultyId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacultyDao",
                table: "FacultyDao");

            migrationBuilder.RenameTable(
                name: "FacultyDao",
                newName: "Faculties");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Faculties",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Professors_Faculties_FacultyId",
                table: "Professors",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Faculties_FacultyId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Faculties",
                table: "Faculties");

            migrationBuilder.RenameTable(
                name: "Faculties",
                newName: "FacultyDao");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "FacultyDao",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacultyDao",
                table: "FacultyDao",
                column: "Id");

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
    }
}
