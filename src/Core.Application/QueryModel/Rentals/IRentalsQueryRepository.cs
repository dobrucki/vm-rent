using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Application.QueryModel.Rentals
{
    public interface IRentalsQueryRepository
    {
        Task<List<RentalQueryEntity>> ListRentalsAsync(int limit, int offset);
        Task<RentalQueryEntity> GetRentalByIdAsync(string id);
    }
}        