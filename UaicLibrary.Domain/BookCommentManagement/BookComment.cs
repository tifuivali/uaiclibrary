using System;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain.BookCommentManagement
{
    public class BookComment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int BookId { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime Date { get; set; }
    }
}
