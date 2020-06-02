using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.EntityConfigurations
{
    public class VirtualMachineEntityTypeConfiguration : IEntityTypeConfiguration<VirtualMachineEntity>
    {
        public void Configure(EntityTypeBuilder<VirtualMachineEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}