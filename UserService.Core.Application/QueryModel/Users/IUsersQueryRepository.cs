using System;
using System.Threading.Tasks;

namespace UserService.Core.Application.QueryModel.Users
{
    public interface IUsersQueryRepository
    {
        Task<UserQueryEntity> GetUserByIdAsync(Guid customerId);
    }
}            