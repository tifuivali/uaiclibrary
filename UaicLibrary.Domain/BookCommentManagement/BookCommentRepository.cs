using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.DataBase.Models;
using UaicLibrary.Domain.GeneralModels;

namespace UaicLibrary.Domain.BookCommentManagement
{
    public class BookCommentRepository : IBookCommentRepository
    {
        private readonly UaicLibraryContext context;
        public BookCommentRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public Result<IList<Comment>> GetCommentsForBook(int bookId)
        {
            var bookDto = context.Books.Include(x => x.Comments).ThenInclude(x => x.User)
                .SingleOrDefault(x => x.Id == bookId);
            if (bookDto == null)
            {
                return Result.Fail<IList<Comment>>("BookDoesNotExists");
            }

            var bookComents = bookDto.Comments.Select(Mapper.Map<Comment>).ToList();

            return Result.Ok<IList<Comment>>(bookComents);

        }

        public Result AddBookComment(BookComment bookComment)
        {
            var bookDto = context.Books.Include(x => x.Comments).SingleOrDefault(x => x.Id == bookComment.BookId);
            if (bookDto == null)
            {
                return Result.Fail("BookDoesNotExists");
            }

            var userDto = context.Users.SingleOrDefault(x => x.Id == bookComment.User.Id);
            var commentBookDto = Mapper.Map<BookCommentDto>(bookComment);
            commentBookDto.User = userDto;
            commentBookDto.Date = DateTime.Now;
            
            bookDto.Comments.Add(commentBookDto);
            context.SaveChanges();

            return Result.Ok();
        }

        public Result RemoveBookComments(int commentId)
        {
            var commentDto = context.BookComments.SingleOrDefault(x => x.Id == commentId);
            if (commentDto == null)
            {
                return Result.Fail("CommentDoesNotExists");
            }

            context.BookComments.Remove(commentDto);
            context.SaveChanges();

            return Result.Ok();
        }

        public Result LikeComment(int commentId)
        {
            var commentDto = context.BookComments.SingleOrDefault(x => x.Id == commentId);
            if (commentDto == null)
            {
                return Result.Fail("CommentDoesNotExists");
            }

            commentDto.Likes++;
            context.SaveChanges();

            return Result.Ok();
        }

        public Result DislikeComment(int commentId)
        {
            var commentDto = context.BookComments.SingleOrDefault(x => x.Id == commentId);
            if (commentDto == null)
            {
                return Result.Fail("CommentDoesNotExists");
            }

            commentDto.Dislikes++;
            context.SaveChanges();

            return Result.Ok();
        }
    }
}
