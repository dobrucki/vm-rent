using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserService.Domain.Models.UserAggregate;
using UserService.Domain.SeedWork;
using UserService.Infrastructure.Entities;
using UserService.Infrastructure.EntityConfigurations;

namespace UserService.Infrastructure
{
    public class UserServiceContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserServiceContext> _logger;

        public const string DEFAULT_SCHEMA = "public";
        public DbSet<User> Users { get; set; }
        public DbSet<Role> UserRoles { get; set; }

        public UserServiceContext(DbContextOptions<UserServiceContext> options, IMediator mediator, ILogger<UserServiceContext> logger) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;
        }
        
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            _logger.LogCritical("Events dispatched");
            await base.SaveChangesAsync(cancellationToken);
            _logger.LogCritical("Committed");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        }
    }
}