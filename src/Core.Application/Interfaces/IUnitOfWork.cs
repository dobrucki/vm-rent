using System;
using Core.Domain.Models;

namespace Core.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<VirtualMachine> VirtualMachines { get; }
        IRepository<Rental> Rentals { get; }

        void Complete();
    }
}