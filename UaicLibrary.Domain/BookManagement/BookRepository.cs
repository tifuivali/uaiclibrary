using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.DataBase.Models;
using UaicLibrary.Domain.AuthorManagement;
using UaicLibrary.Domain.BookCategoryManagement;
using UaicLibrary.Domain.GeneralModels;
using UaicLibrary.Domain.Helpers;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookRepository : IBookRepository
    {
        private readonly UaicLibraryContext context;
        public BookRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public IList<Book> GetAll()
        {
            var bookDtoes = context.Books.Include(x => x.BookAuthors).ThenInclude(y => y.Author).Include(x => x.BookCategory).ToList();
            var books = bookDtoes.Select(Mapper.Map<Book>);
            return books.ToList();
        }

        public IList<Book> GetLatestBooks(int count)
        {
            var bookDtoes = context.Books.Include(x => x.BookAuthors).ThenInclude(y => y.Author).Include(x => x.BookCategory)
                .OrderByDescending(x => x.PublishDate).Take(count).ToList();
            var books = bookDtoes.Select(Mapper.Map<Book>);
            return books.ToList();
        }

        private IQueryable<BookDto> GetBookFullIncludedDataQuery()
        {
            return context.Books
                .Include(x => x.BookAuthors).ThenInclude(x => x.Author)
                .Include(x => x.Faculty)
                .Include(x => x.BookCategory);
        }

        public IList<BookCategory> GetBookCategories()
        {
            var bookCategoriesDtoes = context.BookCategories.ToList();
            return bookCategoriesDtoes.Select(Mapper.Map<BookCategory>).ToList();
        }

        public BookData GetFiltered(BookFilter bookFilter)
        {
            var query = GetBookFullIncludedDataQuery();
            //do staff filter logic
            if (bookFilter.Title?.Length > 0)
            {
                query = query.Where(x => x.Name.Trim().ToUpper().Contains(bookFilter.Title.Trim().ToUpper()));
            }

            if (bookFilter?.Categories?.Count > 0)
            {
                var selectedCategoriesIds = bookFilter.Categories.GetSelectedItemsIds();
                if (selectedCategoriesIds.Any())
                {
                    query = query.Where(x => selectedCategoriesIds.Contains(x.BookCategory.Id));
                }

            }

            if (bookFilter?.Authors?.Count > 0)
            {
                var selectedAuthorsIds = bookFilter.Authors.GetSelectedItemsIds();
                if (selectedAuthorsIds.Any())
                {
                    query = selectedAuthorsIds.Aggregate(query, (current, id) => current.Where(x => x.BookAuthors.Any(a => a.Author.Id == id)));
                }
            }

            if (bookFilter?.Faculties?.Count > 0)
            {
                var selectedFacultiesIds = bookFilter.Faculties.GetSelectedItemsIds();
                if (selectedFacultiesIds.Any())
                {
                    query = query.Where(x => x.Faculty != null && selectedFacultiesIds.Contains(x.Faculty.Id));
                }
            }



            //do pagination from result
            var paginatedQuery = query.Paginating(bookFilter);
            return new BookData()
            {
                Books = paginatedQuery.PaginatedQuey.Select(Mapper.Map<Book>).ToList(),
                Page = bookFilter.Page,
                NumberOfPages = paginatedQuery.NumberOfPages
            };
        }

        public Result Like(int bookId, int userId)
        {
            return LikeBook(bookId, userId, true);
        }

        private Result LikeBook(int bookId, int userId, bool isLike)
        {
            if (context.BookLikes.Any(x => x.Book.Id == bookId && x.User.Id == userId))
            {
                return Result.Ok();
            }

            var bookDto = context.Books.SingleOrDefault(x => x.Id == bookId);
            if (bookDto == null)
            {
                return Result.Fail("BookDoesNotExists");
            }
            var userDto = context.Users.SingleOrDefault(x => x.Id == userId);
            if (userDto == null)
            {
                return Result.Fail("UserDoesNotExits");
            }
            var bookLikeDto = new BookLikeDto()
            {
                Book = bookDto,
                IsLike = isLike,
                User = userDto
            };
            context.BookLikes.Add(bookLikeDto);
            if (isLike)
            {
                bookDto.Likes++;
            }
            else
            {
                bookDto.Dislikes++;
            }
            context.SaveChanges();
            return Result.Ok();
        }

        public Result Dislike(int bookId, int userId)
        {
            return LikeBook(bookId, userId, false);
        }

        public Result OpenBook(int bookId, int userId)
        {
            if (context.OpennedBooks.Any(x => x.Book.Id == bookId && x.User.Id == userId))
            {
                return Result.Ok();
            }
            var bookDto = context.Books.SingleOrDefault(x => x.Id == bookId);
            if (bookDto == null)
            {
                return Result.Fail("BookDoesNotExists");
            }
            var userDto = context.Users.Include(x => x.OpenedBooksCollection).SingleOrDefault(x => x.Id == userId);
            if (userDto == null)
            {
                return Result.Fail("UserDoesNotExists");
            }

            var rededBookDto = new BookOpennedDto()
            {
                Book = bookDto,
                User = userDto,
                Date = DateTime.Now
            };
            context.OpennedBooks.Add(rededBookDto);
            if (userDto.StudentId.HasValue)
            {
                var studentDto = context.Students.SingleOrDefault(x => x.Id == userDto.StudentId.Value);
                if (studentDto != null)
                {
                    studentDto.OpennedBooks = userDto.OpenedBooksCollection.Count();
                }
            }
            if (userDto.ProfesorId.HasValue)
            {
                var professorDto = context.Professors.SingleOrDefault(x => x.Id == userDto.ProfesorId.Value);
                if (professorDto != null)
                {
                    professorDto.OpennedBooks = userDto.OpenedBooksCollection.Count();
                }
            }
            bookDto.Views++;
            context.SaveChanges();
            return Result.Ok();
        }

        public IList<User> GetBookUserLikes(int bookId, bool isLike)
        {
            var userDtoes =
                context.BookLikes.Include(x => x.User)
                    .Where(x => x.Book.Id == bookId && x.IsLike == isLike)
                    .Select(x => x.User).ToList();
            return userDtoes.Select(Mapper.Map<User>).ToList();
        }

        public IList<User> GetBookOpenners(int bookId)
        {
            var bookOpenersDtoes =
                context.OpennedBooks.Include(x => x.User)
                .Where(x => x.Book.Id == bookId)
                .Select(x => x.User);
            return bookOpenersDtoes.Select(Mapper.Map<User>).ToList();
        }

        public Result<Book> GetBookById(int bookId)
        {
            var bookDto = context.Books
                .Include(x => x.BookAuthors).ThenInclude(x => x.Author)
                .Include(x => x.Faculty)
                .Include(x => x.BookCategory)
                .SingleOrDefault(x => x.Id == bookId);
            if (bookDto == null)
            {
                Result.Fail("BookDoesNotExits");
            }
            var book = Mapper.Map<Book>(bookDto);
            if (bookDto != null)
            {
                book.BookAuthors = bookDto.BookAuthors.Select(x => Mapper.Map<Author>(x.Author)).ToList();
            }

            return Result.Ok(book);
        }

        public Result MarkAsRead(int bookId, int userId)
        {
            if (context.BookReaders.Any(x => x.Book.Id == bookId && x.User.Id == userId))
            {
                return Result.Ok();
            }
            var bookDto = context.Books.SingleOrDefault(x => x.Id == bookId);
            if (bookDto == null)
            {
                return Result.Fail("BookDoesNotExits");
            }

            var userDto = context.Users.SingleOrDefault(x => x.Id == userId);
            if (userDto == null)
            {
                return Result.Fail("UserDoesNotExits");
            }
            var bookReaderDto = new BookReadDto()
            {
                Book = bookDto,
                User = userDto,
                Date = DateTime.Now
            };
            context.BookReaders.Add(bookReaderDto);
            bookDto.Reads++;
            if (userDto.StudentId.HasValue)
            {
                var studentDto = context.Students.SingleOrDefault(x => x.Id == userDto.StudentId.Value);
                if (studentDto != null)
                {
                    studentDto.ReadedBooks++;
                }
            }
            if (userDto.ProfesorId.HasValue)
            {
                var profesorDto = context.Professors.SingleOrDefault(x => x.Id == userDto.ProfesorId.Value);
                if (profesorDto != null)
                {
                    profesorDto.ReadedBooks++;
                }
            }

            context.SaveChanges();
            return Result.Ok();
        }

        public IList<User> GetBookReaders(int bookId)
        {
            var bookReadersDtoes =
                context.BookReaders.Include(x => x.User)
                    .Where(x => x.Book.Id == bookId)
                    .Select(x => x.User);
            return bookReadersDtoes.Select(Mapper.Map<User>).ToList();

        }

        public bool IsBookMarkedAsReaded(int bookId, int userId)
        {
            return context.BookReaders.Any(x => x.Book.Id == bookId && x.User.Id == userId);
        }

        public Result<BookPageMark> GetLastSavedPageMark(int bookId, int userId)
        {
            if (!context.BookPageMarks.Any(x => x.Book.Id == bookId && x.User.Id == userId))
            {
                return Result.Fail<BookPageMark>("UserHasNoSavdPageForThisBook");
            }

            var bookPageMarkDto = context.BookPageMarks.SingleOrDefault(x => x.Book.Id == bookId && x.User.Id == userId);
            return bookPageMarkDto == null
                ? Result.Fail<BookPageMark>("BookPageMarkDoesNotFound")
                : Result.Ok(Mapper.Map<BookPageMark>(bookPageMarkDto));
        }

        private Result AddPageMark(BookPageMark bookPageMark)
        {
            var userDto = context.Users.SingleOrDefault(x => x.Id == bookPageMark.UserId);
            if (userDto == null)
            {
                return Result.Fail("UserDoesNotExits");
            }
            var bookDto = context.Books.SingleOrDefault(x => x.Id == bookPageMark.BookId);
            if (bookDto == null)
            {
                return Result.Fail("BookDoesNotExists");
            }
            var bookPageMarkDto1 = Mapper.Map<BookPageMarkDto>(bookPageMark);
            bookPageMarkDto1.User = userDto;
            bookPageMarkDto1.Book = bookDto;
            context.BookPageMarks.Add(bookPageMarkDto1);
            context.SaveChanges();
            return Result.Ok();
        }

        private Result UpdateBookPageMark(BookPageMark bookPageMark)
        {
            var bookPageMarkDto2 =
               context.BookPageMarks.SingleOrDefault(
                   x => x.Book.Id == bookPageMark.BookId && x.User.Id == bookPageMark.UserId);
            if (bookPageMarkDto2 == null)
            {
                return Result.Fail("BookPageMarkDoesNotExits");
            }
            bookPageMarkDto2.Date = bookPageMark.Date;
            bookPageMarkDto2.Page = bookPageMark.Page;
            context.SaveChanges();
            return Result.Ok();
        }

        public Result SavePageMark(BookPageMark bookPageMark)
        {
            if (!context.BookPageMarks.Any(x => x.Book.Id == bookPageMark.BookId && x.User.Id == bookPageMark.UserId))
            {
                return AddPageMark(bookPageMark);
            }

            return UpdateBookPageMark(bookPageMark);
        }

        public Result<BookPageAnnotation> GetBookPageAnnotation(int bookId, int userId, int page)
        {
            if (!context.BookPageAnnotations.Any(x => x.Book.Id == bookId && x.User.Id == userId))
            {
                return Result.Fail<BookPageAnnotation>("UserHasNoSavdPageForThisBook");
            }

            var bookPageAnnotationDto = context.BookPageAnnotations.SingleOrDefault(x => x.Book.Id == bookId && x.User.Id == userId && x.Page == page);
            return bookPageAnnotationDto == null
                ? Result.Fail<BookPageAnnotation>("BookPageAnnotationDoesNotFound")
                : Result.Ok(Mapper.Map<BookPageAnnotation>(bookPageAnnotationDto));
        }

        private void UpdateBookPageAnnotations(BookDto bookDto)
        {
            if (!context.BookPageAnnotations.Any(x => x.Book.Id == bookDto.Id))
            {
                bookDto.PageAnnotations++;
            }
        }

        private void UpdateStudentPageAnnotations(int? studentId)
        {
            if (!studentId.HasValue) return;
            var studentDto = context.Students.SingleOrDefault(x => x.Id == studentId.Value);
            if (studentDto != null)
            {
                studentDto.EditedBooks++;
            }
        }

        private void UpdateProfessorPageAnnotations(int? professorId)
        {
            if (!professorId.HasValue) return;
            var professorDto = context.Professors.SingleOrDefault(x => x.Id == professorId.Value);
            if (professorDto != null)
            {
                professorDto.EditedBooks++;
            }
        }

        private Result AddPageAnnotation(BookPageAnnotation bookPageAnnotation)
        {
            var userDto = context.Users.SingleOrDefault(x => x.Id == bookPageAnnotation.UserId);
            if (userDto == null)
            {
                return Result.Fail("UserDoesNotExits");
            }
            var bookDto = context.Books.SingleOrDefault(x => x.Id == bookPageAnnotation.BookId);
            if (bookDto == null)
            {
                return Result.Fail("BookDoesNotExists");
            }
            var pageAnnotationDto = Mapper.Map<BookPageAnnotationDto>(bookPageAnnotation);
            pageAnnotationDto.User = userDto;
            pageAnnotationDto.Book = bookDto;
            context.BookPageAnnotations.Add(pageAnnotationDto);
            UpdateBookPageAnnotations(bookDto);
            UpdateProfessorPageAnnotations(userDto.ProfesorId);
            UpdateStudentPageAnnotations(userDto.StudentId);
            context.SaveChanges();
            return Result.Ok();
        }

        private Result UpdateBookPageAnnotation(BookPageAnnotation bookPageAnnotation)
        {
            var bookPageAnnotationDto =
               context.BookPageAnnotations.Include(x => x.User)
                    .SingleOrDefault(
                   x => x.Book.Id == bookPageAnnotation.BookId && x.User.Id == bookPageAnnotation.UserId && x.Page == bookPageAnnotation.Page);
            if (bookPageAnnotationDto == null)
            {
                return Result.Fail("BookPageAnnotationDoesNotExits");
            }
            bookPageAnnotationDto.Date = bookPageAnnotationDto.Date;
            bookPageAnnotationDto.Page = bookPageAnnotationDto.Page;
            bookPageAnnotationDto.Content = bookPageAnnotation.Content;
            UpdateProfessorPageAnnotations(bookPageAnnotationDto.User.ProfesorId);
            UpdateStudentPageAnnotations(bookPageAnnotationDto.User.StudentId);
            context.SaveChanges();
            return Result.Ok();
        }

        public Result SaveBookPageAnnotation(BookPageAnnotation bookPageAnnotation)
        {
            if (!context.BookPageAnnotations.Any(x => x.Book.Id == bookPageAnnotation.BookId && x.User.Id == bookPageAnnotation.UserId && x.Page == bookPageAnnotation.Page))
            {
                return AddPageAnnotation(bookPageAnnotation);
            }

            return UpdateBookPageAnnotation(bookPageAnnotation);
        }

        public Result<Book> UpdateBookAuthors(IList<SelectItemModel> selectedAuthors, BookDto bookDto)
        {
            var selectedAuthorIds = selectedAuthors.Where(x => x.IsSelected).Select(x => x.Id).ToList();
            var unSelectedAuthorIds = selectedAuthors.Where(x => !x.IsSelected).Select(x => x.Id).ToList();
            foreach (var selectedAuthorId in selectedAuthorIds)
            {
                if (bookDto.BookAuthors.All(x => x.Author.Id != selectedAuthorId))
                {
                    var authorDto = context.Authors.SingleOrDefault(x => x.Id == selectedAuthorId);
                    if (authorDto != null)
                    {
                        bookDto.BookAuthors.Add(new BookAuthorDto()
                        {
                            Author = authorDto,
                            AuthorId = authorDto.Id,
                            BookId = bookDto.Id,
                            Book = bookDto
                        });
                    }
                }
            }

            foreach (var unselectedAuthorId in unSelectedAuthorIds)
            {
                if (bookDto.BookAuthors.Any(x => x.Author.Id == unselectedAuthorId))
                {
                    var bookAithorDtoToRemove =
                        bookDto.BookAuthors.SingleOrDefault(x => x.Author.Id == unselectedAuthorId);
                    bookDto.BookAuthors.Remove(bookAithorDtoToRemove);
                }    
            }

            var book = Mapper.Map<Book>(bookDto);

            return Result.Ok(book);
        }

        public Result<Book> AddAuthorsToBook(IList<SelectItemModel> selectedAuthors, BookDto bookDto)
        {
            var selectedAuthorIds = selectedAuthors.Where(x => x.IsSelected).Select(x => x.Id).ToList();
            var selectedAuthorDtoes = context.Authors.Where(x => selectedAuthorIds.Contains(x.Id)).ToList();
            var bookAuthorsDtoes = selectedAuthorDtoes.Select(selectedAuthorDto => new BookAuthorDto()
            {
                Author = selectedAuthorDto, AuthorId = selectedAuthorDto.Id, Book = bookDto, BookId = bookDto.Id
            }).ToList();
            bookDto.BookAuthors = bookAuthorsDtoes;

            var book = Mapper.Map<Book>(bookDto);
            return Result.Ok(book);
        }


        public Result<Book> UpdateBook(BookAdministrationModel model)
        {
            var bookDto = context.Books
                .Include(x => x.BookAuthors)
                .ThenInclude(x => x.Author)
                .SingleOrDefault(x => x.Id == model.Id);
            if (bookDto == null) return Result.Fail<Book>("BookDoesNotExists");
            bookDto.Isbn = model.Isbn;
            bookDto.Name = model.Name;
            bookDto.ImageUrl = model.ImageUrl;
            bookDto.BookUrl = model.BookUrl;
            if (model.PublishDate != bookDto.PublishDate)
            {
                model.PublishDate = model.PublishDate.AddDays(1);
            }
            bookDto.Description = model.Description;
            bookDto.NumberOfPages = model.NumberOfPages;
            var bookCategoryDto = context.BookCategories.SingleOrDefault(x => x.Id == model.BookCategoryId);
            if (bookCategoryDto == null) return Result.Fail<Book>("BookCategoryDoesNotExists");
            bookDto.BookCategory = bookCategoryDto;
            bookDto.PublishDate = model.PublishDate;
            var facultyDto = context.Faculties.SingleOrDefault(x => x.Id == model.FacultyId);
            if (facultyDto != null)
            {
                bookDto.Faculty = facultyDto;
            }
            var result = UpdateBookAuthors(model.BookAuthors, bookDto);
            context.SaveChanges();

            return result;
        }

        public Result<Book> CreateBook(BookAdministrationModel model)
        {
            var bookDto = new BookDto();
            model.PublishDate = model.PublishDate.AddDays(1);
            bookDto.Isbn = model.Isbn;
            bookDto.Name = model.Name;
            bookDto.Description = model.Description;
            bookDto.ImageUrl = model.ImageUrl;
            bookDto.BookUrl = model.BookUrl;
            bookDto.NumberOfPages = model.NumberOfPages;
            var bookCategoryDto = context.BookCategories.SingleOrDefault(x => x.Id == model.BookCategoryId);
            if (bookCategoryDto == null) return Result.Fail<Book>("BookCategoryDoesNotExists");
            bookDto.BookCategory = bookCategoryDto;
            bookDto.PublishDate = model.PublishDate;
            var facultyDto = context.Faculties.SingleOrDefault(x => x.Id == model.FacultyId);
            if (facultyDto == null) return Result.Fail<Book>("FacultyDoesNotExists");
            bookDto.Faculty = facultyDto;
            var result = AddAuthorsToBook(model.BookAuthors, bookDto);
            context.Books.Add(bookDto);
            context.SaveChanges();

            return result;
        }

        public Result<Book> SaveBook(BookAdministrationModel bookAdministrationModel)
        {
            
            if (bookAdministrationModel.Id > 0)
            {
                return UpdateBook(bookAdministrationModel);
            }

            return CreateBook(bookAdministrationModel);
        }

        public Result RemoveBookById(int bookId)
        {
            var bookDto = context.Books.SingleOrDefault(x => x.Id == bookId);
            if(bookDto == null) return Result.Fail(Errors.BookDoesNotExist);
            context.Books.Remove(bookDto);
            context.SaveChanges();
            return Result.Ok();
        }
    }
}
