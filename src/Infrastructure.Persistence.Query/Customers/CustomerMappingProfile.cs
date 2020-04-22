using AutoMapper;
using Core.Application.QueryModel.Customers;

namespace Infrastructure.Persistence.Query.Customers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<CustomerEntity, CustomerQueryEntity>();
        }
    }
}