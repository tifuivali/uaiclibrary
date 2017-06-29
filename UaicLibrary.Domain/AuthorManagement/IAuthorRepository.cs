using System.Collections.Generic;
using UaicLibrary.Common.Error;

namespace UaicLibrary.Domain.AuthorManagement
{
    public interface IAuthorRepository
    {
        IList<Author> GetAllAuthors();
        Result<Author> SaveAuthor(Author author);
        Result<Author> GetAuthorById(int authorId);
        Result RemoveAuthorById(int authorId);
    }
}