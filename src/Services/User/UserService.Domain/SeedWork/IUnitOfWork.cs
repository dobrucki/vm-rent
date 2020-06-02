using System;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Domain.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}    