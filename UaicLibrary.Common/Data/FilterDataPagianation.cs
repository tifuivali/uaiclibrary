using System.Collections.Generic;

namespace UaicLibrary.Common.Data
{
    public class FilterDataPagianation<T>
    {
        public int Page { get; set; }
        public int NumberOfPages { get; set; }
        public IList<T> Items { get; set; }
    }
}
