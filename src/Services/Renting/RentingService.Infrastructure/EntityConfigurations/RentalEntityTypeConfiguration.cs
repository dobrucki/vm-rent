using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.EntityConfigurations
{
    public class RentalEntityTypeConfiguration : IEntityTypeConfiguration<Rental>
    {
        public void Configure(EntityTypeBuilder<Rental> builder)
        {
            builder.Ignore(x => x.DomainEvents);
            builder.HasKey(x => x.Id);
        }    
    }
}