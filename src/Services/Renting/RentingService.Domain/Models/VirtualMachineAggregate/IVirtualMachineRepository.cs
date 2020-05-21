using System.Threading.Tasks;
using RentingService.Domain.SeedWork;

namespace RentingService.Domain.Models.VirtualMachineAggregate
{
    public interface IVirtualMachineRepository : IRepository<VirtualMachine>
    {
    }
}