using System;

namespace UserService.Core.Application.QueryModel.Users.Queries
{
    public sealed class GetUserQuery : IQuery<UserQueryEntity>
    {
        public Guid CustomerId { get; set; }
    }
}