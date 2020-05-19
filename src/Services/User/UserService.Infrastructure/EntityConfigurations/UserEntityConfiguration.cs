using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.EntityConfigurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("user");
            builder.HasKey(x => x.UserEntityId).HasName("user_id_pkey");
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.UserEntityId).HasColumnName("id");
            builder.Property(x => x.Password).HasColumnName("password_hash");
            builder.Property(x => x.EmailAddress).HasColumnName("email_address");
            builder.Property(x => x.FirstName).HasColumnName("first_name");
            builder.Property(x => x.LastName).HasColumnName("last_name");
            builder.Property(x => x.IsActive).HasColumnName("is_active");
        }
    }
}