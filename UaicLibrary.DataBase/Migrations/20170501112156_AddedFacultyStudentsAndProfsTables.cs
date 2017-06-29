using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedFacultyStudentsAndProfsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyProfessor_Faculties_FacultyId",
                table: "FacultyProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyProfessor_Professors_ProfessorId",
                table: "FacultyProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyStudent_Faculties_FacultyId",
                table: "FacultyStudent");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyStudent_Students_StudentId",
                table: "FacultyStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacultyStudent",
                table: "FacultyStudent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacultyProfessor",
                table: "FacultyProfessor");

            migrationBuilder.RenameTable(
                name: "FacultyStudent",
                newName: "FacultyStudents");

            migrationBuilder.RenameTable(
                name: "FacultyProfessor",
                newName: "FacultyProfessors");

            migrationBuilder.RenameIndex(
                name: "IX_FacultyStudent_FacultyId",
                table: "FacultyStudents",
                newName: "IX_FacultyStudents_FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_FacultyProfessor_FacultyId",
                table: "FacultyProfessors",
                newName: "IX_FacultyProfessors_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacultyStudents",
                table: "FacultyStudents",
                columns: new[] { "StudentId", "FacultyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacultyProfessors",
                table: "FacultyProfessors",
                columns: new[] { "ProfessorId", "FacultyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyProfessors_Faculties_FacultyId",
                table: "FacultyProfessors",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyProfessors_Professors_ProfessorId",
                table: "FacultyProfessors",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyStudents_Faculties_FacultyId",
                table: "FacultyStudents",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyStudents_Students_StudentId",
                table: "FacultyStudents",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FacultyProfessors_Faculties_FacultyId",
                table: "FacultyProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyProfessors_Professors_ProfessorId",
                table: "FacultyProfessors");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyStudents_Faculties_FacultyId",
                table: "FacultyStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_FacultyStudents_Students_StudentId",
                table: "FacultyStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacultyStudents",
                table: "FacultyStudents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FacultyProfessors",
                table: "FacultyProfessors");

            migrationBuilder.RenameTable(
                name: "FacultyStudents",
                newName: "FacultyStudent");

            migrationBuilder.RenameTable(
                name: "FacultyProfessors",
                newName: "FacultyProfessor");

            migrationBuilder.RenameIndex(
                name: "IX_FacultyStudents_FacultyId",
                table: "FacultyStudent",
                newName: "IX_FacultyStudent_FacultyId");

            migrationBuilder.RenameIndex(
                name: "IX_FacultyProfessors_FacultyId",
                table: "FacultyProfessor",
                newName: "IX_FacultyProfessor_FacultyId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacultyStudent",
                table: "FacultyStudent",
                columns: new[] { "StudentId", "FacultyId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_FacultyProfessor",
                table: "FacultyProfessor",
                columns: new[] { "ProfessorId", "FacultyId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyProfessor_Faculties_FacultyId",
                table: "FacultyProfessor",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyProfessor_Professors_ProfessorId",
                table: "FacultyProfessor",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyStudent_Faculties_FacultyId",
                table: "FacultyStudent",
                column: "FacultyId",
                principalTable: "Faculties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacultyStudent_Students_StudentId",
                table: "FacultyStudent",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
