namespace UserService.Infrastructure.Entities
{
    public class UserEntityRoleEntity
    {
        public int RoleEntityId { get; set; }
        public RoleEntity RoleEntity { get; set; }
        public int UserEntityId { get; set; }
        public UserEntity UserEntity { get; set; }
    }
}