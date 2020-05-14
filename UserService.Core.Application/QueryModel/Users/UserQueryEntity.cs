namespace UserService.Core.Application.QueryModel.Users
{
    public sealed class UserQueryEntity
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}