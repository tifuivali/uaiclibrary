using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using UaicLibrary.Common.Error;
using UaicLibrary.DataBase.Contexts;
using UaicLibrary.DataBase.Models;

namespace UaicLibrary.Domain.AuthorManagement
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly UaicLibraryContext context;
        public AuthorRepository(UaicLibraryContext context)
        {
            this.context = context;
        }

        public IList<Author> GetAllAuthors()
        {
            var authorsDtoes = context.Authors.ToList();
            return authorsDtoes.Select(Mapper.Map<Author>).ToList();
        }

        public Result<Author> CreateAuthor(Author author)
        {
            var authorDto = Mapper.Map<AuthorDto>(author);
            context.Authors.Add(authorDto);
            context.SaveChanges();
            return Result.Ok(Mapper.Map<Author>(authorDto));
        }

        public Result<Author> UpdateAuthor(Author author)
        {
            var authorDto = context.Authors.SingleOrDefault(x => x.Id == author.Id);
            if (authorDto == null) return Result.Fail<Author>(Errors.AuthorDoesNotExist);
            authorDto.DetailsPageUrl = author.DetailsPageUrl;
            authorDto.ImageUrl = author.ImageUrl;
            authorDto.Name = author.Name;
            context.SaveChanges();
            return Result.Ok(Mapper.Map<Author>(authorDto));
        }

        public Result<Author> SaveAuthor(Author author)
        {
            return author.Id > 0 ? UpdateAuthor(author) : CreateAuthor(author);
        }

        public Result<Author> GetAuthorById(int authorId)
        {
            var authorDto = context.Authors.SingleOrDefault(x => x.Id == authorId);
            if (authorDto == null) return Result.Fail<Author>(Errors.AuthorDoesNotExist);
            return Result.Ok(Mapper.Map<Author>(authorDto));
        }

        public Result RemoveAuthorById(int authorId)
        {
            var authorDto = context.Authors.SingleOrDefault(x => x.Id == authorId);
            if (authorDto == null) return Result.Fail<Author>(Errors.AuthorDoesNotExist);
            context.Authors.Remove(authorDto);
            return Result.Ok();
        }
    }
}
