using System.Collections.Generic;
using UaicLibrary.Common.Data;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookData : FilterDataPagianation<Book>
    {
        public IList<Book> Books { get; set; }
    }
}
