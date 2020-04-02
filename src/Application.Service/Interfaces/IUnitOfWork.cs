using System;

namespace Application.Service.Interfaces
{
    using Domain.Models;
    
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Customer> Customers { get; }
        IRepository<VirtualMachine> VirtualMachines { get; }
        IRepository<Rental> Rentals { get; }

        void Complete();
    }
}