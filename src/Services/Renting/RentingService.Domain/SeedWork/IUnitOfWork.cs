using System.Threading;
using System.Threading.Tasks;

namespace RentingService.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
        Task RollbackAsync();
    }
}    