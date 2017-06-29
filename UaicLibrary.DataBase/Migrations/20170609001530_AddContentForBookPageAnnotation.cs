using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddContentForBookPageAnnotation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPageAnnotations_Users_UserDtoId",
                table: "BookPageAnnotations");

            migrationBuilder.RenameColumn(
                name: "UserDtoId",
                table: "BookPageAnnotations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPageAnnotations_UserDtoId",
                table: "BookPageAnnotations",
                newName: "IX_BookPageAnnotations_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "BookPageAnnotations",
                maxLength: 500000,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookPageAnnotations_Users_UserId",
                table: "BookPageAnnotations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookPageAnnotations_Users_UserId",
                table: "BookPageAnnotations");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "BookPageAnnotations");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "BookPageAnnotations",
                newName: "UserDtoId");

            migrationBuilder.RenameIndex(
                name: "IX_BookPageAnnotations_UserId",
                table: "BookPageAnnotations",
                newName: "IX_BookPageAnnotations_UserDtoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookPageAnnotations_Users_UserDtoId",
                table: "BookPageAnnotations",
                column: "UserDtoId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
