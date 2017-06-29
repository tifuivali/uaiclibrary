using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace UaicLibrary.DataBase.Models
{
    public class AuthorDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual ICollection<BookAuthorDto> BookAuthors { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual string DetailsPageUrl { get; set; }
    }
}
