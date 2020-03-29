using Application.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application.Infrastructure.EFDataAccess
{
    public class PostgresContext : DbContext
    {
        public DbSet<VirtualMachine> VirtualMachines { get; set; }

        public PostgresContext(DbContextOptions<PostgresContext> options) : base(options)
        {
            
        }
    }
}