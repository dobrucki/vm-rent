using AutoMapper;
using UserService.Core.Application.QueryModel.Users;

namespace UserService.Infrastructure.Persistence.Query.Users
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserEntity, UserQueryEntity>();
        }
    }
}