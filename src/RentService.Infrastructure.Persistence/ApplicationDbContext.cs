using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using RentService.Infrastructure.Persistence.Entities;

namespace RentService.Infrastructure.Persistence
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