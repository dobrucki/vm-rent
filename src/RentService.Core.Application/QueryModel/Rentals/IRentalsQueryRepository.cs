using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace RentService.Core.Application.QueryModel.Rentals
{
    public interface IRentalsQueryRepository
    {
        Task<List<RentalQueryEntity>> ListRentalsAsync(int limit, int offset);
        Task<List<RentalQueryEntity>> ListCustomerRentalsAsync(int limit, int offset, Guid customerId);
        Task<List<RentalQueryEntity>> ListVirtualMachineRentalsAsync(int limit, int offset, Guid virtualMachineId);
        Task<RentalQueryEntity> GetRentalByIdAsync(string id);
    }
}        