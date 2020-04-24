using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Core.Application.QueryModel.Rentals
{
    public interface IRentalsQueryRepository
    {
        Task<List<RentalQueryEntity>> ListRentalsAsync(int limit, int offset);
        Task<List<RentalQueryEntity>> ListCustomerRentalsAsync(int limit, int offset, Guid customerId);
        Task<RentalQueryEntity> GetRentalByIdAsync(string id);
    }
}        