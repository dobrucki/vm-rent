using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentService.Infrastructure.Persistence.Entities;

namespace RentService.Infrastructure.Persistence.Configuration
{
    public class VirtualMachineEntityConfiguration : IEntityTypeConfiguration<VirtualMachineEntity>
    {
        public void Configure(EntityTypeBuilder<VirtualMachineEntity> builder)
        {
            builder.ToTable("virtual_machine");

            builder.Property(x => x.Id).HasColumnType("uuid").HasColumnName("id");
            builder.HasKey(x => x.Id).HasName("virtual_machine_id_pkey");

            builder.Property(x => x.Name).HasColumnName("name");
        }
    }
}