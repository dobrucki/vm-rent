using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.EntityConfigurations;

namespace UserService.Infrastructure
{
    public class UserServiceContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public const string DEFAULT_SCHEMA = "public";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> UserRoles { get; set; }

        public UserServiceContext(DbContextOptions<UserServiceContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}