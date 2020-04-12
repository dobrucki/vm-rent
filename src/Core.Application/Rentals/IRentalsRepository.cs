using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain.Rentals;

namespace Core.Application.Rentals
{
    public interface IRentalsRepository
    {
        Task<Rental> GetRentalByIdAsync(Guid id);
        Task InsertRentalAsync(Rental rental);
        Task<List<Rental>> ListRentalsAsync(int limit, int offset);
        Task<IEnumerable<Rental>> GetRentalsAsync(Expression<Func<Rental, bool>> filter);
        Task UpdateRangeAsync(IEnumerable<Rental> rentals);
    }
}