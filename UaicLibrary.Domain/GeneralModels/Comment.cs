using System;
using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain.GeneralModels
{
    public class Comment
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Text { get; set; }
        public int Likes { get; set; }
        public int Dislikes { get; set; }
        public DateTime Date { get; set; }
    }
}
