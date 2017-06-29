using System;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookPageMark
    {
        public int Page { get; set; }
        public DateTime Date { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
    }
}
