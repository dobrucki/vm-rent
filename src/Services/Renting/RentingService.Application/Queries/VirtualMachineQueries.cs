using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RentingService.Application.Dtos;
using RentingService.Domain.Models.VirtualMachineAggregate;

namespace RentingService.Application.Queries
{
    public class VirtualMachineQueries
    {
        private readonly IVirtualMachineRepository _virtualMachineRepository;
        private readonly IMapper _mapper;

        public VirtualMachineQueries(IVirtualMachineRepository virtualMachineRepository, IMapper mapper)
        {
            _virtualMachineRepository = virtualMachineRepository
                                        ?? throw new ArgumentNullException(nameof(virtualMachineRepository));
            _mapper = mapper
                      ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<VirtualMachine>> GetVirtualMachines(int limit, int page)
        {
            var virtualMachines = await _virtualMachineRepository
                .GetVirtualMachinesAsync(limit, page-1);
            return virtualMachines;
        }
    }    
}