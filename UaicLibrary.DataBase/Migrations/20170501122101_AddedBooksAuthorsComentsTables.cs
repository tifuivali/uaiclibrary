using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedBooksAuthorsComentsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorDto_AuthorDto_AuthorId",
                table: "BookAuthorDto");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorDto_BookDto_BookId",
                table: "BookAuthorDto");

            migrationBuilder.DropForeignKey(
                name: "FK_BookDto_BookCategoryDto_BookCategoryId",
                table: "BookDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookDto",
                table: "BookDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCategoryDto",
                table: "BookCategoryDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AuthorDto",
                table: "AuthorDto");

            migrationBuilder.RenameTable(
                name: "BookDto",
                newName: "Books");

            migrationBuilder.RenameTable(
                name: "BookCategoryDto",
                newName: "BookCategories");

            migrationBuilder.RenameTable(
                name: "AuthorDto",
                newName: "Authors");

            migrationBuilder.RenameIndex(
                name: "IX_BookDto_BookCategoryId",
                table: "Books",
                newName: "IX_Books_BookCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCategories",
                table: "BookCategories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Authors",
                table: "Authors",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "BookComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BookDtoId = table.Column<int>(nullable: true),
                    Dislikes = table.Column<int>(nullable: false),
                    Likes = table.Column<int>(nullable: false),
                    ProfessorId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    Text = table.Column<string>(maxLength: 4096, nullable: true)
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
                        name: "FK_BookComments_Professors_ProfessorId",
                        column: x => x.ProfessorId,
                        principalTable: "Professors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookComments_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookComments_BookDtoId",
                table: "BookComments",
                column: "BookDtoId");

            migrationBuilder.CreateIndex(
                name: "IX_BookComments_ProfessorId",
                table: "BookComments",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookComments_StudentId",
                table: "BookComments",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorDto_Authors_AuthorId",
                table: "BookAuthorDto",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorDto_Books_BookId",
                table: "BookAuthorDto",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCategories_BookCategoryId",
                table: "Books",
                column: "BookCategoryId",
                principalTable: "BookCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorDto_Authors_AuthorId",
                table: "BookAuthorDto");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthorDto_Books_BookId",
                table: "BookAuthorDto");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCategories_BookCategoryId",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookCategories",
                table: "BookCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Authors",
                table: "Authors");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "BookDto");

            migrationBuilder.RenameTable(
                name: "BookCategories",
                newName: "BookCategoryDto");

            migrationBuilder.RenameTable(
                name: "Authors",
                newName: "AuthorDto");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BookCategoryId",
                table: "BookDto",
                newName: "IX_BookDto_BookCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookDto",
                table: "BookDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookCategoryDto",
                table: "BookCategoryDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AuthorDto",
                table: "AuthorDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorDto_AuthorDto_AuthorId",
                table: "BookAuthorDto",
                column: "AuthorId",
                principalTable: "AuthorDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthorDto_BookDto_BookId",
                table: "BookAuthorDto",
                column: "BookId",
                principalTable: "BookDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookDto_BookCategoryDto_BookCategoryId",
                table: "BookDto",
                column: "BookCategoryId",
                principalTable: "BookCategoryDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
