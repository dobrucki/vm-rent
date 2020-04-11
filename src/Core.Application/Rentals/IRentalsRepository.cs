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
        Task<IEnumerable<Rental>> ListRentalsAsync(int limit, int offset);
    }
}