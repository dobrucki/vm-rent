using System;
using System.Threading.Tasks;
using Core.Domain.Rentals;

namespace Core.Application.Rentals
{
    public interface IRentalsRepository
    {
        Task<Rental> GetRentalByIdAsync(Guid id);
        Task InsertRentalAsync(Rental rental);
    }
}