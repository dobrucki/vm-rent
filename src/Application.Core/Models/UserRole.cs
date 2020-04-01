namespace Application.Core.Models
{
    public class UserRole : ModelBase
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}