using System;
using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;

namespace Core.Application.SharedKernel
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<VirtualMachine> VirtualMachines { get; }
        IRepository<Rental> Rentals { get; }

        void Complete();
    }
}