using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Domain.SeedWork;
using RentingService.Infrastructure.Entities;
using RentingService.Infrastructure.EntityConfigurations;

namespace RentingService.Infrastructure
{
    public class RentingServiceContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<VirtualMachine> VirtualMachines { get; set; }

        public RentingServiceContext(DbContextOptions<RentingServiceContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RentingServiceContext).Assembly);
            // modelBuilder.ApplyConfiguration(new RentalEntityTypeConfiguration());
            // modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            // modelBuilder.ApplyConfiguration(new VirtualMachineEntityTypeConfiguration());
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
        }

        public Task RollbackAsync()
        {
            throw new NotImplementedException();
        }
    }
}