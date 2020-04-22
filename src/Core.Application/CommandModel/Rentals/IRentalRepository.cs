using System.Threading.Tasks;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;

namespace Core.Application.CommandModel.Rentals
{
    public interface IRentalRepository
    {
        Task InsertOneAsync(Rental rental);
    }
}    