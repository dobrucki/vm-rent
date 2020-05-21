using System;
using RentingService.Domain.Models.VirtualMachineAggregate;
using RentingService.Domain.SeedWork;

namespace RentingService.Infrastructure.Repositories
{
    public class VirtualMachineRepository : IVirtualMachineRepository
    {
        private readonly RentingServiceContext _context;
        
        public VirtualMachineRepository(RentingServiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork { get; }


        public void Insert(VirtualMachine virtualMachine)
        {
            _context.VirtualMachines.Add(virtualMachine);
        }
    }
}