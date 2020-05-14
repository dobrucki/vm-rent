namespace UserService.Core.Application.QueryModel.Users.Queries
{
    public class CheckUserCredentialsQuery : IQuery<bool>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}