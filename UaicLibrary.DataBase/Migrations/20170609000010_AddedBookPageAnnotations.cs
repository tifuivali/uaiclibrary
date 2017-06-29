using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedBookPageAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookPageAnnotations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BookId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Page = table.Column<int>(nullable: false),
                    UserDtoId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPageAnnotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookPageAnnotations_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookPageAnnotations_Users_UserDtoId",
                        column: x => x.UserDtoId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPageAnnotations_BookId",
                table: "BookPageAnnotations",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPageAnnotations_UserDtoId",
                table: "BookPageAnnotations",
                column: "UserDtoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPageAnnotations");
        }
    }
}
