using System.Collections.Generic;
using UaicLibrary.Domain.StudentManagement;

namespace UaicLibrary.Domain.UserManagement
{
    public interface IUserProvider
    {
        User GetUserByEmailAndPassword(string email, string password);
        User GetUserBy(string email);
        IList<Student> GetAll();
    }
}