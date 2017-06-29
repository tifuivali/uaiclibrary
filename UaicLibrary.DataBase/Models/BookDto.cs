using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class BookDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        [Required]
        [StringLength(255)]
        public virtual string Isbn { get; set; }
        public virtual ICollection<BookAuthorDto> BookAuthors { get; set; }
        public virtual string Description { get; set; }
        public virtual string Name { get; set; }
        public virtual int NumberOfPages { get; set; }
        public virtual ICollection<BookCommentDto> Comments { get; set; }
        public virtual BookCategoryDto BookCategory { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual int Likes { get; set; }
        public virtual int Dislikes { get; set; }
        public virtual FacultyDao Faculty { get; set; }
        public virtual ICollection<BookOpennedDto> Opens { get; set; }
        public virtual ICollection<BookLikeDto> BookLikes { get; set; }
        public virtual ICollection<BookReadDto> BookReaders { get; set; }
        public virtual ICollection<BookPageMarkDto> BookPageMarks { get; set; }
        public virtual ICollection<BookPageAnnotationDto> BookPageAnntoations { get; set; }
        public virtual int Views { get; set; }
        public virtual int Reads { get; set; }
        public virtual int PageAnnotations { get; set; }
        [StringLength(2046)]
        public virtual string BookUrl { get; set; }
    }
}
