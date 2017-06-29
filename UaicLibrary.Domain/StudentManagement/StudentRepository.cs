using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.Domain.FacultyManagement;

namespace UaicLibrary.Domain.StudentManagement
{
    public class StudentRepository : IStudentRepository
    {
        private readonly UaicLibraryContext context;
        public StudentRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public Result<Student> GetStudentByEmail(string email)
        {
            var studentDto = context.Students.Include(s => s.FacultyStudents).ThenInclude(fs => fs.Faculty).SingleOrDefault(x => x.Email == email);
            if (studentDto != null)
            {
                var facultyStudent = studentDto.FacultyStudents.Select(x => x.Faculty);
                var student = Mapper.Map<Student>(studentDto);
                var studentFaculties = facultyStudent.Select(Mapper.Map<Faculty>).ToList();
                student.Faculties = studentFaculties;
                return Result.Ok(student);

            }
            return Result.Fail<Student>("StudentNotExists");
        }

        public Result UpdateStudentDescription(int studentId , string description)
        {
            var studentDto = context.Students.SingleOrDefault(x => x.Id == studentId);
            if (studentDto == null)
            {
                return Result.Fail("StudentNotExists");
            }
            var userDto = context.Users.SingleOrDefault(x => x.StudentId == studentId);
            if (userDto == null)
            {
                return Result.Fail("UserDoesNotExits");
            }

            userDto.About = description;
            studentDto.About = description;
            context.SaveChanges();
            return Result.Ok();
        }

        public void UpdateAvatarUrl(int studentId, string url)
        {
            var studentDto = context.Students.SingleOrDefault(x => x.Id == studentId);
            if (studentDto != null)
            {
                studentDto.ImageUrl = url;
            }
            var userDto = context.Users.SingleOrDefault(x => x.StudentId == studentId);
            if (userDto != null)
            {
                userDto.ImageUrl = url;
            }

            context.SaveChanges();
        }

        public void UpdateDescription(int studentId, string description)
        {
            var studentDto = context.Students.SingleOrDefault(x => x.Id == studentId);
            if (studentDto == null) return;
            studentDto.About = description;
            context.SaveChanges();
        }
    }
}
