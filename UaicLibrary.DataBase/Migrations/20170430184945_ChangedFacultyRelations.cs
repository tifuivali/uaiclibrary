using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class ChangedFacultyRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professors_Faculties_FacultyId",
                table: "Professors");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyId",
                table: "Students");

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

            migrationBuilder.CreateTable(
                name: "FacultyProfessor",
                columns: table => new
                {
                    ProfessorId = table.Column<int>(nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyProfessor", x => new { x.ProfessorId, x.FacultyId });
                    table.ForeignKey(
                        name: "FK_FacultyProfessor_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyProfessor_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FacultyStudent",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyStudent", x => new { x.StudentId, x.FacultyId });
                    table.ForeignKey(
                        name: "FK_FacultyStudent_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyStudent_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacultyProfessor_FacultyId",
                table: "FacultyProfessor",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyStudent_FacultyId",
                table: "FacultyStudent",
                column: "FacultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacultyProfessor");

            migrationBuilder.DropTable(
                name: "FacultyStudent");

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Students",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FacultyId",
                table: "Professors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyId",
                table: "Students",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_FacultyId",
                table: "Professors",
                column: "FacultyId");

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
    }
}
