using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<CustomerEntity>
    {
        public void Configure(EntityTypeBuilder<CustomerEntity> builder)
        {
            builder.ToTable("customer");

            builder.Property(x => x.Id).HasColumnType("uuid").HasColumnName("id");
            builder.HasKey(x => x.Id).HasName("customer_id_pkey");

            builder.Property(x => x.EmailAddress).HasColumnName("email_address");

            builder.Property(x => x.FirstName).HasColumnName("first_name");

            builder.Property(x => x.LastName).HasColumnName("last_name");
        }
    }
}