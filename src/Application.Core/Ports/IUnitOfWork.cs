using System;
using System.Threading.Tasks;
using Application.Core.Models;

namespace Application.Core.Ports
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<VirtualMachine> VirtualMachines { get; }

        Task<int> Complete();
    }
}