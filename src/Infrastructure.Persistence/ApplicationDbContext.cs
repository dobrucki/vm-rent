using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<VirtualMachine> VirtualMachines { get; }
        public DbSet<Customer> Customers { get; }
        public DbSet<Rental> Rentals { get; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}