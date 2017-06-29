using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain
{
    public interface IGeneralRepository
    {
        int GetStudentId(string email);
        User GetUserByEmail(string email);
    }
}