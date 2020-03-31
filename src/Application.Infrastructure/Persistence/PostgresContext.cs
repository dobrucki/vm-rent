using Application.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Infrastructure.Persistence
{
    public class PostgresContext : DbContext
    {
        public DbSet<VirtualMachine> VirtualMachines { get; set; }
        public DbSet<User> Users { get; set; }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
            
        }
    }
}