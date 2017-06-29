using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class RenameReadedBooksTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBooks_Books_BookId",
                table: "ReadedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_ReadedBooks_Users_UserId",
                table: "ReadedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReadedBooks",
                table: "ReadedBooks");

            migrationBuilder.RenameTable(
                name: "ReadedBooks",
                newName: "OpennedBooks");

            migrationBuilder.RenameIndex(
                name: "IX_ReadedBooks_UserId",
                table: "OpennedBooks",
                newName: "IX_OpennedBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReadedBooks_BookId",
                table: "OpennedBooks",
                newName: "IX_OpennedBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OpennedBooks",
                table: "OpennedBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OpennedBooks_Books_BookId",
                table: "OpennedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OpennedBooks_Users_UserId",
                table: "OpennedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OpennedBooks_Books_BookId",
                table: "OpennedBooks");

            migrationBuilder.DropForeignKey(
                name: "FK_OpennedBooks_Users_UserId",
                table: "OpennedBooks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OpennedBooks",
                table: "OpennedBooks");

            migrationBuilder.RenameTable(
                name: "OpennedBooks",
                newName: "ReadedBooks");

            migrationBuilder.RenameIndex(
                name: "IX_OpennedBooks_UserId",
                table: "ReadedBooks",
                newName: "IX_ReadedBooks_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_OpennedBooks_BookId",
                table: "ReadedBooks",
                newName: "IX_ReadedBooks_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReadedBooks",
                table: "ReadedBooks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBooks_Books_BookId",
                table: "ReadedBooks",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReadedBooks_Users_UserId",
                table: "ReadedBooks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
