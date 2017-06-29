using System.Collections.Generic;

namespace UaicLibrary.DataBase.Models
{
    public class UserDto
    {
        public virtual int Id { get; set; }
        public virtual string MatricolNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string Role { get; set; }
        public virtual string LastName { get; set; }
        public virtual string About { get; set; }
        public virtual int Year { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual int? StudentId { get; set; }
        public virtual int? ProfesorId { get; set; }
        public virtual ICollection<BookOpennedDto> OpenedBooksCollection { get; set; }
        public virtual ICollection<BookLikeDto> BookLikes { get; set; }
        public virtual ICollection<BookReadDto> ReadedBooks { get; set; }
        public virtual ICollection<BookPageMarkDto> BookPageMarks { get; set; }
        public virtual ICollection<BookPageAnnotationDto> BookPageAnntoations { get; set; }
    }
}
