using System.Linq;
using AutoMapper;
using UaicLibrary.DataBase.Models;
using UaicLibrary.Domain.AuthorManagement;
using UaicLibrary.Domain.BookCategoryManagement;
using UaicLibrary.Domain.BookCommentManagement;
using UaicLibrary.Domain.BookManagement;
using UaicLibrary.Domain.FacultyManagement;
using UaicLibrary.Domain.GeneralModels;
using UaicLibrary.Domain.ProfessorManagement;
using UaicLibrary.Domain.SettingsManagement;
using UaicLibrary.Domain.StudentManagement;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain.AutoMapper
{
    public class AutoMapperGeneralProfile : Profile
    {
        public AutoMapperGeneralProfile()
        {
            CreateMap<StudentDto, Student>()
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => $"{x.FirstName} {x.LastName}"));
            CreateMap<Student, StudentDto>();
            CreateMap<FacultyDao, Faculty>().ReverseMap();
            CreateMap<BookCategoryDto, BookCategory>().ReverseMap();
            CreateMap<AuthorDto, Author>().ReverseMap();
            CreateMap<BookCommentDto, BookComment>();
            CreateMap<BookDto, Book>()
                .ForMember(x => x.BookAuthors, opt => opt.MapFrom(x => x.BookAuthors.Select(y => y.Author)));
            CreateMap<ProfessorDto, Professor>().ReverseMap();
            CreateMap<ProfessorDto, User>().ReverseMap();
            CreateMap<StudentDto, User>().ReverseMap();
            CreateMap<BookCommentDto, Comment>().ReverseMap();
            CreateMap<BookComment, BookCommentDto>().ReverseMap();
            CreateMap<BookPageMarkDto, BookPageMark>().ReverseMap();
            CreateMap<BookPageAnnotationDto, BookPageAnnotation>().ReverseMap();
            CreateMap<BookPageAnnotationDto, BookAnnotedModel>();
            CreateMap<SettingDto, Setting>().ReverseMap();
        }
    }
}

