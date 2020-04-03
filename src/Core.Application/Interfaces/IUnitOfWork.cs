using System;
using Core.Domain.Models;
using Core.Domain.Models.Entities;

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