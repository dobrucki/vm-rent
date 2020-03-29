using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;

namespace Application.Infrastructure.EFDataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostgresContext _context;

        public UnitOfWork(PostgresContext context)
        {
            _context = context;
            VirtualMachines = new Repository<VirtualMachine>(_context);
        }
        
        public IRepository<VirtualMachine> VirtualMachines { get; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }
        
        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}