using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.DataBase.Models;

namespace UaicLibrary.Domain.FacultyManagement
{
    public class FacultyRepository : IFacultyRepository
    {
        private readonly UaicLibraryContext context;
        public FacultyRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public IList<Faculty> GetAllFaculties()
        {
            return context.Faculties.Select(Mapper.Map<Faculty>).ToList();
        }

        public Result<Faculty> GetFacultyById(int facultyId)
        {
            var facultyDto = context.Faculties.SingleOrDefault(x => x.Id == facultyId);
            if (facultyDto == null) return Result.Fail<Faculty>(Errors.FacultyDoesNotExits);
            return Result.Ok(Mapper.Map<Faculty>(facultyDto));
        }

        private Result<Faculty> CreateFaculty(Faculty faculty)
        {
            var facultyDto = Mapper.Map<FacultyDao>(faculty);
            context.Faculties.Add(facultyDto);
            context.SaveChanges();
            return Result.Ok(Mapper.Map<Faculty>(facultyDto));
        }

        private Result<Faculty> UpdateFaculty(Faculty faculty)
        {
            var facultyDto = context.Faculties.SingleOrDefault(x => x.Id == faculty.Id);
            if (facultyDto == null) return Result.Fail<Faculty>(Errors.FacultyDoesNotExits);
            facultyDto.Address = faculty.Address;
            facultyDto.Name = faculty.Name;
            context.SaveChanges();
            return Result.Ok(Mapper.Map<Faculty>(facultyDto));
        }

        public Result<Faculty> SaveFaculty(Faculty faculty)
        {
            return faculty.Id > 0 ? UpdateFaculty(faculty) : CreateFaculty(faculty);
        }

        public Result RemoveFacultyById(int facultyId)
        {
            var facultyDto = context.Faculties.SingleOrDefault(x => x.Id == facultyId);
            if (facultyDto == null) return Result.Fail<Faculty>(Errors.FacultyDoesNotExits);
            context.Faculties.Remove(facultyDto);
            context.SaveChanges();
            return Result.Ok();
        }
    }
}
