using System.Collections.Generic;
using UaicLibrary.Common.Filters;
using UaicLibrary.Domain.GeneralModels;

namespace UaicLibrary.Domain.BookManagement
{
    public class BookFilter : PaginationFilter
    {
        public string Title { get; set; }
        public IList<SelectItemModel> Categories { get; set; }
        public IList<SelectItemModel> Authors { get; set; }
        public IList<SelectItemModel> Faculties { get; set; }
    }
}