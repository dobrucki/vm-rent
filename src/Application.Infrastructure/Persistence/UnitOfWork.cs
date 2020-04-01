using System;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;

namespace Application.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresContext _context;
        private IRepository<VirtualMachine> _virtualMachineRepository;
        private IRepository<Reservation> _reservationRepository;

        public UnitOfWork(PostgresContext context)
        {
            _context = context;
        }

        public IRepository<VirtualMachine> VirtualMachines => 
            _virtualMachineRepository ??= new Repository<VirtualMachine>(_context);

        public IRepository<Reservation> Reservations =>
            _reservationRepository ??= new Repository<Reservation>(_context);

        public void Complete()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

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