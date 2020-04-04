using System;
using Core.Domain.Models.Entities;

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