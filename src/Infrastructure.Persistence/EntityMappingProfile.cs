using AutoMapper;
using Core.Domain.Customers;
using Core.Domain.Rentals;
using Core.Domain.VirtualMachines;
using Infrastructure.Persistence.Entities;

namespace Infrastructure.Persistence
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Customer, CustomerEntity>().ReverseMap();
            CreateMap<VirtualMachine, VirtualMachineEntity>().ReverseMap();
            CreateMap<Rental, RentalEntity>().ReverseMap();
        }
    }
}