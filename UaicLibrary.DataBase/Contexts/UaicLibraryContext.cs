using Microsoft.EntityFrameworkCore;
using UaicLibrary.DataBase.Models;

namespace UaicLibrary.DataBase.Contexts
{
    public class UaicLibraryContext : DbContext
    {
        public DbSet<UserDto> Users { get; set; }
        public DbSet<ProfessorDto> Professors { get; set; }
        public DbSet<StudentDto> Students { get; set; }
        public DbSet<MatricolNumberDto> MatricolNumbers { get; set; }
        public DbSet<FacultyDao> Faculties { get; set; }
        public DbSet<FacultyStudent> FacultyStudents { get; set; }
        public DbSet<FacultyProfessor> FacultyProfessors { get; set; }
        public DbSet<BookDto> Books { get; set; }
        public DbSet<AuthorDto> Authors { get; set; }
        public DbSet<BookCategoryDto> BookCategories { get; set; }
        public DbSet<BookCommentDto> BookComments { get; set; }
        public DbSet<BookOpennedDto> OpennedBooks { get; set; }
        public DbSet<BookLikeDto> BookLikes { get; set; }
        public DbSet<BookReadDto> BookReaders { get; set; }
        public DbSet<BookPageMarkDto> BookPageMarks { get; set; }
        public DbSet<BookPageAnnotationDto> BookPageAnnotations { get; set; }
        public DbSet<BookAuthorDto> BookAuthors { get; set; }
        public DbSet<SettingDto> Settings { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseNpgsql(DataBaseConnectionString.LocalPgSqlConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<StudentDto>().Property(x => x.Id).ForNpgsqlUseSequenceHiLo();
            modelBuilder.Entity<ProfessorDto>().Property(x => x.Id).ForNpgsqlUseSequenceHiLo();
            modelBuilder.Entity<FacultyDao>().Property(x => x.Id).ForNpgsqlUseSequenceHiLo();

            modelBuilder.Entity<FacultyStudent>()
                .HasKey(ps => new { ps.StudentId, ps.FacultyId });
            modelBuilder.Entity<FacultyStudent>()
                .HasOne(ps => ps.Faculty)
                .WithMany(p => p.FacultyStudents)
                .HasForeignKey(ps => ps.FacultyId);
            modelBuilder.Entity<FacultyStudent>()
                .HasOne(ps => ps.Student)
                .WithMany(p => p.FacultyStudents)
                .HasForeignKey(ps => ps.StudentId);

            modelBuilder.Entity<FacultyProfessor>()
               .HasKey(ps => new { ps.ProfessorId, ps.FacultyId });
            modelBuilder.Entity<FacultyProfessor>()
                .HasOne(ps => ps.Faculty)
                .WithMany(p => p.FacultyProfessors)
                .HasForeignKey(ps => ps.FacultyId);
            modelBuilder.Entity<FacultyProfessor>()
                .HasOne(ps => ps.Professor)
                .WithMany(p => p.FacultyProfessors)
                .HasForeignKey(ps => ps.ProfessorId);

            modelBuilder.Entity<BookAuthorDto>()
              .HasKey(ps => new { ps.BookId, ps.AuthorId });
            modelBuilder.Entity<BookAuthorDto>()
                .HasOne(ps => ps.Book)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(ps => ps.BookId);
            modelBuilder.Entity<BookAuthorDto>()
                .HasOne(ps => ps.Author)
                .WithMany(p => p.BookAuthors)
                .HasForeignKey(ps => ps.AuthorId);
        }
    }
}
