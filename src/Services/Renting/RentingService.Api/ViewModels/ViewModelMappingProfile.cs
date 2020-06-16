using AutoMapper;
using RentingService.Domain.Models.VirtualMachineAggregate;

namespace RentingService.Api.ViewModels
{
    public class ViewModelMappingProfile : Profile
    {
        public ViewModelMappingProfile()
        {
            CreateMap<VirtualMachine, VirtualMachineViewModel>();
        }
    }
}