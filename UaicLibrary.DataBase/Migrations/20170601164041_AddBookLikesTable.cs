using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddBookLikesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLikeDto_Books_BookId",
                table: "BookLikeDto");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLikeDto_Users_UserId",
                table: "BookLikeDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookLikeDto",
                table: "BookLikeDto");

            migrationBuilder.RenameTable(
                name: "BookLikeDto",
                newName: "BookLikes");

            migrationBuilder.RenameIndex(
                name: "IX_BookLikeDto_UserId",
                table: "BookLikes",
                newName: "IX_BookLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLikeDto_BookId",
                table: "BookLikes",
                newName: "IX_BookLikes_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookLikes",
                table: "BookLikes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLikes_Books_BookId",
                table: "BookLikes",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLikes_Users_UserId",
                table: "BookLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLikes_Books_BookId",
                table: "BookLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_BookLikes_Users_UserId",
                table: "BookLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookLikes",
                table: "BookLikes");

            migrationBuilder.RenameTable(
                name: "BookLikes",
                newName: "BookLikeDto");

            migrationBuilder.RenameIndex(
                name: "IX_BookLikes_UserId",
                table: "BookLikeDto",
                newName: "IX_BookLikeDto_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLikes_BookId",
                table: "BookLikeDto",
                newName: "IX_BookLikeDto_BookId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookLikeDto",
                table: "BookLikeDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLikeDto_Books_BookId",
                table: "BookLikeDto",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLikeDto_Users_UserId",
                table: "BookLikeDto",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
