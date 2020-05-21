using System;

namespace RentingService.Application.Queries
{
    public class OffsetPagination
    {
        public int PageNumber { get; }
        public int RowsPerPage { get; }

        public OffsetPagination(int pageNumber, int rowsPerPage)
        {
            if (pageNumber < 1) throw new ArgumentException(nameof(pageNumber) + "is less than 1.");
            if (rowsPerPage < 1) throw new ArgumentException(nameof(rowsPerPage) + "is less than 1.");
            PageNumber = pageNumber;
            RowsPerPage = rowsPerPage;
        }
    }
}