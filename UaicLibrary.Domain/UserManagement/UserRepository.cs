using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.DataBase.Models;
using UaicLibrary.Domain.BookManagement;

namespace UaicLibrary.Domain.UserManagement
{
    public class UserRepository : IUserRepository
    {
        private readonly UaicLibraryContext uaicLibraryContext;
        private readonly IModelValidator<User> userValidator;

        public UserRepository(UaicLibraryContext uaicLibraryContext, IModelValidator<User> userValidator)
        {
            this.uaicLibraryContext = uaicLibraryContext;
            this.userValidator = userValidator;
        }

        public Result<User> CreateUser(User user)
        {
            var validationResult = userValidator.Validate(user);
            if (validationResult.IsFailure)
                return Result.Fail<User>(validationResult.Errors);

            if (user.Role == UserRole.Student)
            {
                var studentDto = Mapper.Map<StudentDto>(user);
                uaicLibraryContext.Students.Add(studentDto);
                uaicLibraryContext.SaveChanges();
                studentDto.FacultyStudents = new List<FacultyStudent>();
                foreach (var faculty in user.Faculties)
                {
                    var facultyDto = uaicLibraryContext.Faculties.SingleOrDefault(x => x.Id == faculty.Id);
                    if (facultyDto != null)
                    {
                        var facultyStudent = new FacultyStudent()
                        {
                            FacultyId = faculty.Id,
                            StudentId = studentDto.Id,
                            Student = studentDto,
                            Faculty = facultyDto

                        };
                        studentDto.FacultyStudents.Add(facultyStudent);
                    }
                }
                var userDto = Mapper.Map<UserDto>(user);
                userDto.StudentId = studentDto.Id;
                uaicLibraryContext.Users.Add(userDto);
                uaicLibraryContext.SaveChanges();
                return Result.Ok(Mapper.Map<User>(studentDto));
            }
            if (user.Role == UserRole.Professor)
            {
                var profesorDto = Mapper.Map<ProfessorDto>(user);
                uaicLibraryContext.Professors.Add(profesorDto);
                uaicLibraryContext.SaveChanges();
                profesorDto.FacultyProfessors = new List<FacultyProfessor>();
                foreach (var faculty in user.Faculties)
                {
                    var facultyDto = uaicLibraryContext.Faculties.SingleOrDefault(x => x.Id == faculty.Id);
                    if (facultyDto != null)
                    {
                        var facultyProfessor = new FacultyProfessor()
                        {
                            FacultyId = faculty.Id,
                            ProfessorId = profesorDto.Id,
                            Professor = profesorDto,
                            Faculty = facultyDto

                        };
                        profesorDto.FacultyProfessors.Add(facultyProfessor);
                    }
                }
                var userDto = Mapper.Map<UserDto>(user);
                userDto.StudentId = profesorDto.Id;
                uaicLibraryContext.Users.Add(userDto);
                uaicLibraryContext.SaveChanges();
                return Result.Ok(Mapper.Map<User>(profesorDto));
            }

            return Result.Fail<User>("InvalidRole");
        }

        public IList<Book> GetLatestOpennedBooks(int cont, int userId)
        {
            var latestOpennedBooksDtoes =
                uaicLibraryContext.OpennedBooks.Include(x => x.Book)
                    .Where(x => x.User.Id == userId)
                    .OrderByDescending(x => x.Date)
                    .Select(x => x.Book)
                    .Take(cont);
            return latestOpennedBooksDtoes.Select(Mapper.Map<Book>).ToList();
        }

        public IList<BookAnnotedModel> GetLatestAnnotedBooks(int cont, int userId)
        {
            return uaicLibraryContext.BookPageAnnotations
                .Include(x => x.User)
                .Include(x => x.Book)
                .Where(X => X.User.Id == userId)
                .OrderByDescending(x => x.Date)
                .Select(Mapper.Map<BookAnnotedModel>)
                .Take(cont)
                .ToList();
        }
    }
}
