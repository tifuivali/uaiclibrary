using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UaicLibrary.DataBase.Contexts;

namespace UaicLibrary.DataBase.Migrations
{
    [DbContext(typeof(UaicLibraryContext))]
    [Migration("20170227184541_ChangedIdForUser")]
    partial class ChangedIdForUser
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

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("MatricolNumber");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });
        }
    }
}
