namespace Application.Core.Models
{
    public class User : ModelBase
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}