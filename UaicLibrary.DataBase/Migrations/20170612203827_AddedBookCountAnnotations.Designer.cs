using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using UaicLibrary.DataBase.Contexts;

namespace UaicLibrary.DataBase.Migrations
{
    [DbContext(typeof(UaicLibraryContext))]
    [Migration("20170612203827_AddedBookCountAnnotations")]
    partial class AddedBookCountAnnotations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:Sequence:.EntityFrameworkHiLoSequence", "'EntityFrameworkHiLoSequence', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("UaicLibrary.DataBase.Models.AuthorDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookAuthorDto", b =>
                {
                    b.Property<int>("BookId");

                    b.Property<int>("AuthorId");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("BookAuthorDto");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookCategoryDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookCommentDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("BookDtoId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Dislikes");

                    b.Property<int>("Likes");

                    b.Property<string>("Text")
                        .HasMaxLength(4096);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookDtoId");

                    b.HasIndex("UserId");

                    b.ToTable("BookComments");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("BookCategoryId");

                    b.Property<string>("BookUrl")
                        .HasMaxLength(2046);

                    b.Property<string>("Description");

                    b.Property<int>("Dislikes");

                    b.Property<int?>("FacultyId");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("Likes");

                    b.Property<string>("Name");

                    b.Property<int>("NumberOfPages");

                    b.Property<int>("PageAnnotations");

                    b.Property<DateTime>("PublishDate");

                    b.Property<int>("Reads");

                    b.Property<int>("Views");

                    b.HasKey("Id");

                    b.HasIndex("BookCategoryId");

                    b.HasIndex("FacultyId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookLikeDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("BookId");

                    b.Property<bool>("IsLike");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookLikes");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookOpennedDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("BookId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("OpennedBooks");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookPageAnnotationDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookId");

                    b.Property<string>("Content")
                        .HasMaxLength(500000);

                    b.Property<DateTime>("Date");

                    b.Property<int>("Page");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookPageAnnotations");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookPageMarkDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Page");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookPageMarks");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookReadDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("BookId");

                    b.Property<DateTime>("Date");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("BookReaders");
                });

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

                    b.Property<int>("EditedBooks");

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

                    b.Property<int>("OpennedBooks");

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<int>("ReadedBooks");

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

                    b.Property<int>("EditedBooks");

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

                    b.Property<int>("OpennedBooks");

                    b.Property<string>("Password")
                        .HasMaxLength(50);

                    b.Property<int>("ReadedBooks");

                    b.Property<int?>("Year");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.UserDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("LastName");

                    b.Property<string>("MatricolNumber");

                    b.Property<string>("Password");

                    b.Property<int?>("ProfesorId");

                    b.Property<string>("Role");

                    b.Property<int?>("StudentId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookAuthorDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.AuthorDto", "Author")
                        .WithMany("BookAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("UaicLibrary.DataBase.Models.BookDto", "Book")
                        .WithMany("BookAuthors")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookCommentDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookDto")
                        .WithMany("Comments")
                        .HasForeignKey("BookDtoId");

                    b.HasOne("UaicLibrary.DataBase.Models.UserDto", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookCategoryDto", "BookCategory")
                        .WithMany()
                        .HasForeignKey("BookCategoryId");

                    b.HasOne("UaicLibrary.DataBase.Models.FacultyDao", "Faculty")
                        .WithMany("Books")
                        .HasForeignKey("FacultyId");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookLikeDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookDto", "Book")
                        .WithMany("BookLikes")
                        .HasForeignKey("BookId");

                    b.HasOne("UaicLibrary.DataBase.Models.UserDto", "User")
                        .WithMany("BookLikes")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookOpennedDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookDto", "Book")
                        .WithMany("Opens")
                        .HasForeignKey("BookId");

                    b.HasOne("UaicLibrary.DataBase.Models.UserDto", "User")
                        .WithMany("OpenedBooksCollection")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookPageAnnotationDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookDto", "Book")
                        .WithMany("BookPageAnntoations")
                        .HasForeignKey("BookId");

                    b.HasOne("UaicLibrary.DataBase.Models.UserDto", "User")
                        .WithMany("BookPageAnntoations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookPageMarkDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookDto", "Book")
                        .WithMany("BookPageMarks")
                        .HasForeignKey("BookId");

                    b.HasOne("UaicLibrary.DataBase.Models.UserDto", "User")
                        .WithMany("BookPageMarks")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("UaicLibrary.DataBase.Models.BookReadDto", b =>
                {
                    b.HasOne("UaicLibrary.DataBase.Models.BookDto", "Book")
                        .WithMany("BookReaders")
                        .HasForeignKey("BookId");

                    b.HasOne("UaicLibrary.DataBase.Models.UserDto", "User")
                        .WithMany("ReadedBooks")
                        .HasForeignKey("UserId");
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
