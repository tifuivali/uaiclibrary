using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UaicLibrary.DataBase.Migrations
{
    public partial class AddedVoewsAndBookCountersForBoooksAndProfesor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EditedBooks",
                table: "Professors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OpennedBooks",
                table: "Professors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReadedBooks",
                table: "Professors",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                table: "Books",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EditedBooks",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "OpennedBooks",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "ReadedBooks",
                table: "Professors");

            migrationBuilder.DropColumn(
                name: "Views",
                table: "Books");
        }
    }
}
