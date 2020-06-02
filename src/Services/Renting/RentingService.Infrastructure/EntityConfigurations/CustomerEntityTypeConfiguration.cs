using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.EntityConfigurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}