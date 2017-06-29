using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedBooksAuthorsComents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthorDto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookCategoryDto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategoryDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookDto",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BookCategoryId = table.Column<int>(nullable: true),
                    Isbn = table.Column<string>(maxLength: 255, nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NumberOfPages = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookDto_BookCategoryDto_BookCategoryId",
                        column: x => x.BookCategoryId,
                        principalTable: "BookCategoryDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookAuthorDto",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false),
                    AuthorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookAuthorDto", x => new { x.BookId, x.AuthorId });
                    table.ForeignKey(
                        name: "FK_BookAuthorDto_AuthorDto_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AuthorDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookAuthorDto_BookDto_BookId",
                        column: x => x.BookId,
                        principalTable: "BookDto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthorDto_AuthorId",
                table: "BookAuthorDto",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookDto_BookCategoryId",
                table: "BookDto",
                column: "BookCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookAuthorDto");

            migrationBuilder.DropTable(
                name: "AuthorDto");

            migrationBuilder.DropTable(
                name: "BookDto");

            migrationBuilder.DropTable(
                name: "BookCategoryDto");
        }
    }
}
