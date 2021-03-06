using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentingService.Domain.Models.RentalAggregate;

namespace RentingService.Application.Queries
{
    public class RentalQueries
    {
        private readonly IRentalRepository _userRepository;

        public RentalQueries(IRentalRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Rental> GetRentalByIdQuery(Guid id)
        {
            var rental = await _userRepository
                .GetRentalByIdAsync(id) ?? throw new Exception();
            return rental;
        }

        public async Task<IEnumerable<Rental>> GetRentalsQuery()
        {
            return await _userRepository.GetRentalsAsync(50, 0);
        }
        
    }    
}