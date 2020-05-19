using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.EntityConfigurations
{
    public class RoleEntityConfiguration : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.ToTable("role");
            builder.HasKey(x => x.RoleEntityId).HasName("role_id_pkey");
            builder.Property(x => x.RoleName).HasColumnName("name");
            builder.Property(x => x.RoleEntityId).HasColumnName("id");
        }
    }
}