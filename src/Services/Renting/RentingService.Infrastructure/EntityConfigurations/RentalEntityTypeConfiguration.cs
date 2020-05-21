using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.Models.VirtualMachineAggregate;

namespace RentingService.Infrastructure.EntityConfigurations
{
    public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            builder.HasKey(x => x.Id);
            
            builder.HasOne(x => x.Customer)
                .WithMany()
                .IsRequired();
            // builder.HasOne<VirtualMachine>()
            //     .WithMany()
            //     .IsRequired()
            //     .HasForeignKey(x => x.VirtualMachineId);
        }    
    }
}