using System.Collections.Generic;
using UaicLibrary.Common.Error;

namespace UaicLibrary.Domain.FacultyManagement
{
    public interface IFacultyRepository
    {
        IList<Faculty> GetAllFaculties();
        Result<Faculty> GetFacultyById(int facultyId);
        Result<Faculty> SaveFaculty(Faculty faculty);
        Result RemoveFacultyById(int facultyId);
    }
}