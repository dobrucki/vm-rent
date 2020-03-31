using System;
using System.Threading.Tasks;
using Application.Core.Models;

namespace Application.Core.Ports
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<VirtualMachine> VirtualMachines { get; }

        void Complete();
    }
}