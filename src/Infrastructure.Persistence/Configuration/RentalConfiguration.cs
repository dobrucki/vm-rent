using Core.Domain.Rentals;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class RentalConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.ToTable("rental");

            builder.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();
            
            builder.Property(x => x.CustomerId)
                .HasColumnName("id")
                .ValueGeneratedNever();
            
            builder.Property(x => x.VirtualMachineId)
                .HasColumnName("id")
                .ValueGeneratedNever();
        }
    }
}