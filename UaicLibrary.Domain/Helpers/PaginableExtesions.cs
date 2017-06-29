using System.Collections.Generic;
using System.Linq;
using UaicLibrary.Common.Data;
using UaicLibrary.Common.Filters;

namespace UaicLibrary.Domain.Helpers
{
    public static class PaginableExtesions
    {
        public static FilterQueryPagination<T> Paginating<T>(this IQueryable<T> query, PaginationFilter filter)
        {
            var numberOfItensToShip = filter.PageSize * (filter.Page - 1);
            //do pagination from result
            var numberOfPages = 0;
            var count = query.Count();

            if (count == 0)
            {
                return new FilterQueryPagination<T>()
                {
                    NumberOfPages = 1,
                    Page = 1,
                    PaginatedQuey = query
                };
            }
            if (count % filter.PageSize == 0)
            {
                numberOfPages = count / filter.PageSize;
            }
            else
            {
                numberOfPages = count / filter.PageSize + 1;
            }

            var paginatedQuery = query.Skip(numberOfItensToShip).Take(filter.PageSize);

            return new FilterQueryPagination<T>()
            {
                Page = filter.Page,
                NumberOfPages = numberOfPages,
                PaginatedQuey = paginatedQuery
            };
        }

        public static FilterDataPagianation<T> Paginating<T>(this IList<T> items, PaginationFilter filter)
        {
            var numberOfItensToShip = filter.PageSize * (filter.Page - 1);
            //do pagination from result
            var numberOfPages = 0;
            var count = items.Count();
            if (count % filter.PageSize == 0)
            {
                numberOfPages = count / filter.PageSize;
            }
            else
            {
                numberOfPages = count / filter.PageSize + 1;
            }

            var paginatedItems = items.Skip(numberOfItensToShip).Take(filter.PageSize).ToList();

            return new FilterDataPagianation<T>()
            {
                Page = filter.Page,
                NumberOfPages = numberOfPages,
                Items = paginatedItems
            };
        }
    }
}
