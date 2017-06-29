using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.BookManagement;
using UaicLibrary.Web.Helpers;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{

    namespace UaicLibrary.Web.Controllers
    {
        [EnableCors(CorsPolicy.AllowSpecificOrigin)]
        [Route("api/[controller]/[action]")]
        public class BookController : BaseApiController
        {
            private readonly IBookRepository bookRepository;

            public BookController(IBookRepository bookRepository)
            {
                this.bookRepository = bookRepository;
            }

            [HttpGet]
            public ActionResult GetAll()
            {
                var books = bookRepository.GetAll();
                return Ok(books);
            }

            [HttpGet]
            public ActionResult GetFiltered(BookFilter filter)
            {
                var bookData = bookRepository.GetFiltered(filter);
                return Ok(bookData);
            }

            [HttpPost]
            public ActionResult Filter([FromBody] BookFilter filter)
            {
                var bookData = bookRepository.GetFiltered(filter);
                return Ok(bookData);
            }

            [HttpGet]
            public ActionResult GetBookById(int bookId)
            {
                var result = bookRepository.GetBookById(bookId);
                return ValidationResult(result);
            }

            [HttpGet]
            [AllowAnonymous]
            public ActionResult GetLatest(int count)
            {
                var books = bookRepository.GetLatestBooks(count);
                return Ok(books);
            }

            [HttpGet]
            public ActionResult GetCategories()
            {
                var booksCategories = bookRepository.GetBookCategories();
                return Ok(booksCategories);
            }

            [HttpPost]
            public ActionResult Like([FromBody] int bookId)
            {
                var result = bookRepository.Like(bookId, CurrentUser.Id);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult Dislike([FromBody] int bookId)
            {
                var result = bookRepository.Dislike(bookId, CurrentUser.Id);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult OpenBook([FromBody] int bookId)
            {
                var result = bookRepository.OpenBook(bookId, CurrentUser.Id);
                return ValidationResult(result);
            }

            [HttpGet]
            public ActionResult GetBookUserLikes(int bookId)
            {
                var bookUserLikes = bookRepository.GetBookUserLikes(bookId, true);
                return Ok(bookUserLikes);
            }

            [HttpGet]
            public ActionResult GetBookUserDislikes(int bookId)
            {
                var bookUserLikes = bookRepository.GetBookUserLikes(bookId, false);
                return Ok(bookUserLikes);
            }

            [HttpGet]
            public ActionResult GetBookOpenners(int bookId)
            {
                var bookOpenners = bookRepository.GetBookOpenners(bookId);
                return Ok(bookOpenners);
            }

            [AllowAnonymous]
            [HttpGet]
            public async Task<ActionResult> Download(int bookId)
            {
                var result = bookRepository.GetBookById(bookId);
                if (result.IsFailure)
                {
                    return BadRequest("BookDoesNotExits");
                }
                var book = result.Value;
                if (string.IsNullOrEmpty(book.BookUrl))
                {
                    return BadRequest("BookDoesNotHaveAcontent");
                }

                var bookStream = await FileHelper.GetBooktream(book.BookUrl);
                return File(bookStream, "application/pdf",book.Name + ".pdf");

            }

            [HttpGet]
            public ActionResult MarkAsRead(int bookId)
            {
                var result = bookRepository.MarkAsRead(bookId, CurrentUser.Id);
                return ValidationResult(result);
            }

            [HttpGet]
            public ActionResult GetReaders(int bookId)
            {
                var usersReaders = bookRepository.GetBookReaders(bookId);
                return Ok(usersReaders);
            }

            [HttpGet]
            public ActionResult IsBookMarkedARead(int bookid)
            {
                var isBookMarked = bookRepository.IsBookMarkedAsReaded(bookid, CurrentUser.Id);
                return Ok(isBookMarked);
            }

            [HttpGet]
            public ActionResult GetLastSavedPageMark(int bookId)
            {
                var result = bookRepository.GetLastSavedPageMark(bookId, CurrentUser.Id);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult SaveBookPageMark([FromBody] BookPageMark bookPageMark)
            {
                bookPageMark.UserId = CurrentUser.Id;
                bookPageMark.Date = DateTime.Now;
                var result = bookRepository.SavePageMark(bookPageMark);
                return ValidationResult(result);
            }

            [HttpGet]
            public ActionResult GetBookPageAnnotationContent(int bookId, int page)
            {
                var result = bookRepository.GetBookPageAnnotation(bookId, CurrentUser.Id, page);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult SaveBookPageAnnotation([FromBody] BookPageAnnotation bookPageAnnotation)
            {
                bookPageAnnotation.UserId = CurrentUser.Id;
                bookPageAnnotation.Date = DateTime.Now;
                var result = bookRepository.SaveBookPageAnnotation(bookPageAnnotation);
                return ValidationResult(result);
            }

        }
    }

}
