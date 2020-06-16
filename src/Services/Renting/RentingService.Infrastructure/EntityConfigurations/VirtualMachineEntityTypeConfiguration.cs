using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.EntityConfigurations
{
    public class VirtualMachineEntityTypeConfiguration : IEntityTypeConfiguration<VirtualMachine>
    {
        public void Configure(EntityTypeBuilder<VirtualMachine> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            builder.HasKey(x => x.Id);
        }
    }
}