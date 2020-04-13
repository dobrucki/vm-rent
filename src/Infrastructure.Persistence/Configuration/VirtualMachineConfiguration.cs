using Core.Domain.VirtualMachines;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class VirtualMachineConfiguration : IEntityTypeConfiguration<VirtualMachine>
    {
        public void Configure(EntityTypeBuilder<VirtualMachine> builder)
        {
            builder.ToTable("virtual_machine");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

            builder.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50);
        }
    }
}