using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentService.Infrastructure.Persistence.Entities;

namespace RentService.Infrastructure.Persistence.Configuration
{
    public class RentalEntityConfiguration : IEntityTypeConfiguration<RentalEntity>
    {
        public void Configure(EntityTypeBuilder<RentalEntity> builder)
        {
            builder.ToTable("rental");

            builder.Property(x => x.Id).HasColumnType("uuid").HasColumnName("id");
            builder.HasKey(x => x.Id).HasName("rental_id_pkey");

            builder.Property(x => x.StartTime).HasColumnName("start_time");

            builder.Property(x => x.EndTime).HasColumnName("end_time");

            builder.HasOne(x => x.Customer)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.CustomerId)
                .HasConstraintName("customer_id_fkey");

            builder.HasOne(x => x.VirtualMachine)
                .WithMany(x => x.Rentals)
                .HasForeignKey(x => x.VirtualMachineId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("virtual_machine_id_fkey");
        }
    }
}