using System.Collections.Generic;
using System.Linq;

namespace UserService.Application.Queries
{
    public static class PaginationExtension
    {
        public static IEnumerable<TSource> Paginate<TSource>(this IEnumerable<TSource> collection, OffsetPagination pagination)
        {
            return collection
                .Skip((pagination.PageNumber - 1) * pagination.RowsPerPage)
                .Take(pagination.RowsPerPage);
        }
        
    }
}