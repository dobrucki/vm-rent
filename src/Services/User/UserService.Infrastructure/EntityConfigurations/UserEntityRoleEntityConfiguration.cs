using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.EntityConfigurations
{
    public class UserEntityRoleEntityConfiguration : IEntityTypeConfiguration<UserEntityRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntityRoleEntity> builder)
        {
            builder.ToTable("user_role");
            builder.Property(x => x.RoleEntityId).HasColumnName("role_id");
            builder.Property(x => x.UserEntityId).HasColumnName("user_id");
            builder.HasKey(x => new {x.UserEntityId, RoleEntityId = x.RoleEntityId})
                .HasName("user_role_user_id_role_id_pkey");

            builder.HasOne(x => x.RoleEntity)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.RoleEntityId)
                .HasConstraintName("user_role_role_id_fkey");

            builder.HasOne(x => x.UserEntity)
                .WithMany(x => x.UserRoles)
                .HasForeignKey(x => x.UserEntityId)
                .HasConstraintName("user_role_user_id_fkey");
        }
    }
}