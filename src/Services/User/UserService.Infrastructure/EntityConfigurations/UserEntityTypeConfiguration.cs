using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Infrastructure.EntityConfigurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user", UserServiceContext.DEFAULT_SCHEMA);
            builder.Ignore(x => x.DomainEvents);
            builder.HasKey(x => x.Id)
                .HasName("user_id_pkey");

            builder.Property(x => x.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            builder.Property(x => x.Password)
                .HasColumnName("password");
            builder.Property(x => x.EmailAddress)
                .HasColumnName("email_address");
            builder.Property(x => x.FirstName)
                .HasColumnName("first_name");
            builder.Property(x => x.LastName)
                .HasColumnName("last_name");
            builder.Property(x => x.IsActive)
                .HasColumnName("is_active");

            builder.OwnsMany(x => x.Roles, b =>
            {
                b.WithOwner()
                    .HasForeignKey("UserId")
                    .HasConstraintName("user_user_id_fkey");
                b.ToTable("user_role", UserServiceContext.DEFAULT_SCHEMA);
                b.Property(x => x.RoleName).HasColumnName("name");
                b.Property<Guid>("UserId").HasColumnName("user_id");
                b.HasKey("UserId", "RoleName");
            });
        }    
    }
}