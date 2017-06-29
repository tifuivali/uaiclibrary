using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedReadedBooksForuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    About = table.Column<string>(maxLength: 1024, nullable: true),
                    EditedBooks = table.Column<int>(nullable: false),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    FirstName = table.Column<string>(maxLength: 255, nullable: true),
                    ImageUrl = table.Column<string>(maxLength: 255, nullable: true),
                    LastName = table.Column<string>(maxLength: 255, nullable: true),
                    MatricolNumber = table.Column<string>(maxLength: 30, nullable: true),
                    OpennedBooks = table.Column<int>(nullable: false),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    ReadedBooks = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    About = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MatricolNumber = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    ProfesorId = table.Column<int>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FacultyStudents",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false),
                    FacultyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyStudents", x => new { x.StudentId, x.FacultyId });
                    table.ForeignKey(
                        name: "FK_FacultyStudents_Faculties_FacultyId",
                        column: x => x.FacultyId,
                        principalTable: "Faculties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyStudents_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BookDtoId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Dislikes = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 4096, nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookComments_Books_BookDtoId",
                        column: x => x.BookDtoId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReadedBooks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BookId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReadedBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReadedBooks_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReadedBooks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookComments_BookDtoId",
                table: "BookComments",
                column: "BookDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_BookComments_UserId",
                table: "BookComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadedBooks_BookId",
                table: "ReadedBooks",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_ReadedBooks_UserId",
                table: "ReadedBooks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyStudents_FacultyId",
                table: "FacultyStudents",
                column: "FacultyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookComments");

            migrationBuilder.DropTable(
                name: "ReadedBooks");

            migrationBuilder.DropTable(
                name: "FacultyStudents");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
