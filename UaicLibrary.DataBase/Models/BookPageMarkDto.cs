using System;

namespace UaicLibrary.DataBase.Models
{
    public class BookPageMarkDto
    {
        public virtual int Id { get; set; }
        public virtual BookDto Book { get; set; }
        public virtual UserDto User { get; set; }
        public virtual int Page { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
