using System;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookPageAnnotation
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Page { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
