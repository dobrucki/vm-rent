using Application.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.EfCore
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