using System;
using System.ComponentModel.DataAnnotations;

namespace UaicLibrary.DataBase.Models
{
    public class BookPageAnnotationDto
    {
        public virtual int Id { get; set; }
        public virtual BookDto Book { get; set; }
        public virtual UserDto User { get; set; }
        public virtual int Page { get; set; }
        [StringLength(500000)]
        public virtual string Content { get; set; }
        public virtual DateTime Date { get; set; }
    }
}
