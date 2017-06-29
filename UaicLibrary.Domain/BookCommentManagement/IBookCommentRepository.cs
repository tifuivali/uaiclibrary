using System.Collections.Generic;
using UaicLibrary.Common.Error;
using UaicLibrary.Domain.GeneralModels;

namespace UaicLibrary.Domain.BookCommentManagement
{
    public interface IBookCommentRepository
    {
        Result<IList<Comment>> GetCommentsForBook(int bookId);
        Result AddBookComment(BookComment bookComment);
        Result RemoveBookComments(int commentId);
        Result LikeComment(int commentId);
        Result DislikeComment(int commentId);
    }
}