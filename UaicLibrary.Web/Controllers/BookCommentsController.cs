using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using UaicLibrary.Domain.BookCommentManagement;
using UaicLibrary.Web.Security;

namespace UaicLibrary.Web.Controllers
{

    namespace UaicLibrary.Web.Controllers
    {
        [EnableCors(CorsPolicy.AllowSpecificOrigin)]
        [Route("api/[controller]/[action]")]
        public class BookCommentsController : BaseApiController
        {
            private readonly IBookCommentRepository bookCommentRepository;
            public BookCommentsController(IBookCommentRepository bookCommentRepository)
            {
                this.bookCommentRepository = bookCommentRepository;
            }

            [HttpGet]
            public ActionResult GetBookComments(int bookId)
            {
                var result = bookCommentRepository.GetCommentsForBook(bookId);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult AddBookComment([FromBody] BookComment bookComment)
            {
                var result = bookCommentRepository.AddBookComment(bookComment);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult RemoveComment([FromBody] int commentId)
            {
                var result = bookCommentRepository.RemoveBookComments(commentId);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult LikeComment([FromBody] int commentId)
            {
                var result = bookCommentRepository.LikeComment(commentId);
                return ValidationResult(result);
            }

            [HttpPost]
            public ActionResult DislikeComment([FromBody] int commentId)
            {
                var result = bookCommentRepository.DislikeComment(commentId);
                return ValidationResult(result);
            }
        }
    }

}
