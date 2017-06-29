using UaicLibrary.Common.Error;

namespace UaicLibrary.Domain.StudentManagement
{
    public interface IStudentRepository
    {
        Result<Student> GetStudentByEmail(string email);
        Result UpdateStudentDescription(int studentId , string description);
        void UpdateAvatarUrl(int studentId, string url);
        void UpdateDescription(int studentId, string description);
    }
}