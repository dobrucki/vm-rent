using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.QueryModel.Rentals;

namespace Infrastructure.Persistence.Query.Rentals
{
    public class RentalRepository : IRentalsQueryRepository
    {
        public Task<List<RentalQueryEntity>> ListRentalsAsync(int limit, int offset)
        {
            throw new System.NotImplementedException();
        }

        public Task<RentalQueryEntity> GetRentalByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}