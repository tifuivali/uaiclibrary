using System.Collections.Generic;
using UaicLibrary.Common.Error;
using UaicLibrary.Domain.BookManagement;

namespace UaicLibrary.Domain.UserManagement
{
    public interface IUserRepository
    {
        Result<User> CreateUser(User user);
        IList<Book> GetLatestOpennedBooks(int cont, int userId);
        IList<BookAnnotedModel> GetLatestAnnotedBooks(int cont, int userId);
    }
}