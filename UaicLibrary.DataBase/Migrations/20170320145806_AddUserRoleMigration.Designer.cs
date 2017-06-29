using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UaicLibrary.DataBase.Contexts;

namespace UaicLibrary.DataBase.Migrations
{
    [DbContext(typeof(UaicLibraryContext))]
    [Migration("20170320145806_AddUserRoleMigration")]
    partial class AddUserRoleMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:Sequence:.EntityFrameworkHiLoSequence", "'EntityFrameworkHiLoSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("UaicLibrary.DataBase.Models.StudentDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<string>("MatricolNumber")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
