using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<VirtualMachineEntity> VirtualMachines { get; }
        public DbSet<CustomerEntity> Customers { get; }
        public DbSet<RentalEntity> Rentals { get; }
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}