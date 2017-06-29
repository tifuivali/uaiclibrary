using System.Linq;

namespace UaicLibrary.Common.Data
{
    public class FilterQueryPagination<T>
    {
        public int Page { get; set; }
        public int NumberOfPages { get; set; }
        public IQueryable<T> PaginatedQuey { get; set; }
    }
}
