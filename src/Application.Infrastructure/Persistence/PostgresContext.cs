using Application.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence
{
    public class PostgresContext : DbContext
    {
        public DbSet<VirtualMachine> VirtualMachines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
            
        }
    }
}