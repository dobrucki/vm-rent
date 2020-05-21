using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.RentalAggregate
{
    public interface IRentalRepository : IRepository<Rental>
    {
        Task<Rental> GetRentalByIdAsync(Guid id);
        Task<IEnumerable<Rental>> GetRentalsWhereAsync(Expression<Func<Rental, bool>> predicate);
        Task InsertRentalAsync(Rental rental);
    }
}