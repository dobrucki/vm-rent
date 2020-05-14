using System.Threading;
using System.Threading.Tasks;
using UserService.Core.Application.QueryModel.Users.Queries;
using UserService.Core.Application.SharedKernel.Exceptions;

namespace UserService.Core.Application.QueryModel.Users
{
    internal sealed class UserQueryHandler : 
        IQueryHandler<GetUserQuery, UserQueryEntity>
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

    }
}