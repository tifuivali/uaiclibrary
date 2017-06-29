using System;
using System.Collections.Generic;
using UaicLibrary.Domain.AuthorManagement;
using UaicLibrary.Domain.BookCategoryManagement;
using UaicLibrary.Domain.BookCommentManagement;
using UaicLibrary.Domain.FacultyManagement;

namespace UaicLibrary.Domain.BookManagement
{
    public class Book
    {
        public int Id { get; set; }
        public string Isbn { get; set; }
        public IList<Author> BookAuthors { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int NumberOfPages { get; set; }
        public IList<BookComment> Comments { get; set; }
        public BookCategory BookCategory { get; set; }
        public DateTime PublishDate { get; set; }
        public string ImageUrl { get; set; }
        public  int Likes { get; set; }
        public  int Dislikes { get; set; }
        public Faculty Faculty { get; set; }
        public string BookUrl { get; set; }
        public int Views { get; set; }
        public int Reads { get; set; }
        public int PageAnnotations { get; set; }
    }
}
