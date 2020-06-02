using AutoMapper;
using RentingService.Domain.Models.CustomerAggregate;
using RentingService.Domain.Models.RentalAggregate;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure
{
    public class RentingServiceProfile : Profile
    {
        public RentingServiceProfile()
        {
            CreateMap<Rental, RentalEntity>().ReverseMap();
            CreateMap<Customer, CustomerEntity>().ReverseMap();
            CreateMap<VirtualMachine, VirtualMachineEntity>().ReverseMap();
        }
    }
}