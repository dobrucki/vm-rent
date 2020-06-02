using System;
using System.Threading.Tasks;
using AutoMapper;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Domain.SeedWork;
using RentingService.Infrastructure.Entities;

namespace RentingService.Infrastructure.Repositories
{
    public class VirtualMachineRepository : IVirtualMachineRepository
    {
        private readonly RentingServiceContext _context;
        private readonly IMapper _mapper;
        
        public VirtualMachineRepository(RentingServiceContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => _context;


        public async Task InsertVirtualMachineAsync(VirtualMachine virtualMachine)
        {
            var entity = _mapper.Map<VirtualMachineEntity>(virtualMachine);
            _context.VirtualMachines.Add(entity);
        }
    }
}