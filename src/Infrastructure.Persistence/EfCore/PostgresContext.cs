using Core.Domain.Models;
using Core.Domain.Models.Entities;
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