using System.Collections.Generic;
using UaicLibrary.Common.Error;
using UaicLibrary.Domain.BookCategoryManagement;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain.BookManagement
{
    public interface IBookRepository
    {
        IList<Book> GetAll();
        Result<Book> GetBookById(int bookId);
        IList<Book> GetLatestBooks(int count);
        IList<BookCategory> GetBookCategories();
        BookData GetFiltered(BookFilter bookFilter);
        Result Like(int bookId, int userId);
        Result Dislike(int bookId, int userId);
        Result OpenBook(int bookId, int userId);
        IList<User> GetBookUserLikes(int bookId, bool isLike);
        IList<User> GetBookOpenners(int bookId);
        Result MarkAsRead(int bookId, int userId);
        IList<User> GetBookReaders(int bookId);
        bool IsBookMarkedAsReaded(int bookId, int userId);
        Result<BookPageMark> GetLastSavedPageMark(int bookId, int userId);
        Result SavePageMark(BookPageMark bookPageMark);
        Result<BookPageAnnotation> GetBookPageAnnotation(int bookId, int userId, int page);
        Result SaveBookPageAnnotation(BookPageAnnotation bookPageAnnotation);
        Result<Book> SaveBook(BookAdministrationModel bookAdministrationModel);
        Result RemoveBookById(int bookId);
    }
}