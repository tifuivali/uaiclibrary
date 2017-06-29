using UaicLibrary.Domain.UserManagement;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookAnnotedModel
    {
            public int Id { get; set; }
            public Book Book { get; set; }
            public User User { get; set; }
            public int Page { get; set; }
    }
}
