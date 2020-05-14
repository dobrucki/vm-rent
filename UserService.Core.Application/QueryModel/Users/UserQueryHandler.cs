using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Application.QueryModel.Users.Queries;
using UserService.Core.Application.SharedKernel;
using UserService.Core.Application.SharedKernel.Exceptions;

namespace UserService.Core.Application.QueryModel.Users
{
    internal sealed class UserQueryHandler : 
        IQueryHandler<GetUserQuery, UserQueryEntity>,
        IQueryHandler<CheckUserCredentialsQuery, bool>
    {
        private readonly IUsersQueryRepository _users;

        public UserQueryHandler(IUsersQueryRepository users)
        {
            _users = users;
        }


        public async Task<UserQueryEntity> Handle(
            GetUserQuery request, CancellationToken cancellationToken)
        {
            var customer = await _users.GetUserByIdAsync(request.CustomerId);
            if (customer is null)
            {
                throw new NotFoundException("User", request.CustomerId);
            }
            return customer;
        }

        public async Task<bool> Handle(
            CheckUserCredentialsQuery request, CancellationToken cancellationToken)
        {
            var user = await _users.GetUserByEmail(request.Email);
            var hash = Hashing.HashPassword(request.Password);
            return hash.Equals(user.PasswordHash);
        }

    }
}