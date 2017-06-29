namespace UaicLibrary.DataBase.Models
{
    public class BookAuthorDto
    {
        public virtual int BookId { get;set; }
        public virtual BookDto Book { get; set; }
        public virtual int AuthorId { get; set; }
        public virtual AuthorDto Author { get; set; }
    }
}
