﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UaicLibrary.DataBase.Contexts;

namespace UaicLibrary.DataBase.Migrations
{
    [DbContext(typeof(UaicLibraryContext))]
    [Migration("20170501112156_AddedFacultyStudentsAndProfsTables")]
    partial class AddedFacultyStudentsAndProfsTables
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:Sequence:.EntityFrameworkHiLoSequence", "'EntityFrameworkHiLoSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("UaicLibrary.DataBase.Models.FacultyDao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("Address")
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.FacultyProfessor", b =>
                {
                    b.Property<int>("ProfessorId");

                    b.Property<int>("FacultyId");

                    b.HasKey("ProfessorId", "FacultyId");

                    b.HasIndex("FacultyId");

                    b.ToTable("FacultyProfessors");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.FacultyStudent", b =>
                {
                    b.Property<int>("StudentId");

                    b.Property<int>("FacultyId");

                    b.HasKey("StudentId", "FacultyId");

                    b.HasIndex("FacultyId");

                    b.ToTable("FacultyStudents");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.MatricolNumberDto", b =>
                {
                    b.Property<string>("Matricol")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Matricol");

                    b.ToTable("MatricolNumbers");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.ProfessorDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("About")
                        .HasMaxLength(1024);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<string>("MatricolNumber")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Professors");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.StudentDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:HiLoSequenceName", "EntityFrameworkHiLoSequence")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SequenceHiLo);

                    b.Property<string>("About")
                        .HasMaxLength(1024);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .HasMaxLength(255);

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(255);

                    b.Property<string>("LastName")
                        .HasMaxLength(255);

                    b.Property<string>("MatricolNumber")
                        .HasMaxLength(30);

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.FacultyProfessor", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.FacultyDao", "Faculty")
                        .WithMany("FacultyProfessors")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UaicLibrary.DataBase.Models.ProfessorDto", "Professor")
                        .WithMany("FacultyProfessors")
                        .HasForeignKey("ProfessorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.FacultyStudent", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.FacultyDao", "Faculty")
                        .WithMany("FacultyStudents")
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UaicLibrary.DataBase.Models.StudentDto", "Student")
                        .WithMany("FacultyStudents")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
