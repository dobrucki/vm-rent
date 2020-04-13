using Core.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("customer");
            
            builder.Property(x => x.Id)
                .HasColumnName("id")
                .HasColumnType("uuid")
                .ValueGeneratedNever();

            builder.Property(x => x.EmailAddress)
                .HasColumnName("email_address")
                .HasMaxLength(60);

            builder.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(40);

            builder.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(40);
        }
    }
}