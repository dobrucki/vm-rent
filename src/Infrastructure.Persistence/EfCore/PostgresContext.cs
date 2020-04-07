using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.EfCore
{
    public class PostgresContext : DbContext
    {
        public DbSet<VirtualMachine> VirtualMachines { get; }
        public DbSet<Customer> Customers { get; }
        public DbSet<Rental> Rentals { get; }
        
        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
            
        }
    }
}