using System;

namespace Application.Infrastructure.EfCore
{
    using Domain.Models;
    using Service.Interfaces;
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresContext _context;

        public IRepository<VirtualMachine> VirtualMachines =>
            _virtualMachines ??= new Repository<VirtualMachine>(_context);

        public IRepository<Customer> Customers => 
            _customers ??= new Repository<Customer>(_context);

        public IRepository<Rental> Rentals => 
            _rentals ??= new Repository<Rental>(_context);

        public UnitOfWork(PostgresContext context)
        {   
            _context = context;
        }

        public void Complete()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        private IRepository<VirtualMachine> _virtualMachines;
        private IRepository<Customer> _customers;
        private IRepository<Rental> _rentals;

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                _context.Dispose();
            }
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}