using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Application.CommandModel.VirtualMachines;
using Core.Application.QueryModel.VirtualMachines;
using Core.Application.SharedKernel.Exceptions;
using Core.Domain.VirtualMachines;

namespace Core.Application.Test.Mock
{
    public class MockVirtualMachineRepository :
        IVirtualMachineRepository,
        IVirtualMachinesQueryRepository
    {
        private readonly IList<MockVirtualMachineEntity> _virtualMachines = new List<MockVirtualMachineEntity>();
        
        public Task<VirtualMachine> GetByIdAsync(Guid id)
        {
            var virtualMachineEntity = _virtualMachines
                .SingleOrDefault(x => x.Id == id);
            if (virtualMachineEntity is null) throw new NotFoundException("VirtualMachine", id);
            return Task.FromResult(new VirtualMachine
            {
                Id = virtualMachineEntity.Id,
                Name = virtualMachineEntity.Name
            });
        }

        public Task InsertOneAsync(VirtualMachine virtualMachine)
        {
            var virtualMachineEntity = new MockVirtualMachineEntity
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            };
            _virtualMachines.Add(virtualMachineEntity);
            return Task.CompletedTask;
        }

        public Task UpdateOneAsync(VirtualMachine virtualMachine)
        {
            var virtualMachineEntity = _virtualMachines
                .SingleOrDefault(x => x.Id == virtualMachine.Id);
            if (virtualMachineEntity is null) throw new NotFoundException("VirtualMachine", virtualMachine.Id);
            virtualMachineEntity.Name = virtualMachine.Name;
            return Task.CompletedTask;
        }

        public Task DeleteOneAsync(VirtualMachine virtualMachine)
        {
            var virtualMachineEntity = _virtualMachines
                .SingleOrDefault(x => x.Id == virtualMachine.Id);
            if (virtualMachineEntity is null) throw new NotFoundException("VirtualMachine", virtualMachine.Id);
            _virtualMachines.Remove(virtualMachineEntity);
            return Task.CompletedTask;
        }

        public Task<VirtualMachineQueryEntity> GetVirtualMachineByIdAsync(Guid virtualMachineId)
        {
            var virtualMachineEntity = _virtualMachines
                .SingleOrDefault(x => x.Id == virtualMachineId);
            if (virtualMachineEntity is null) throw new NotFoundException("VirtualMachine", virtualMachineId);
            return Task.FromResult(new VirtualMachineQueryEntity
            {
                Id = virtualMachineEntity.Id.ToString(),
                Name = virtualMachineEntity.Name
            });
        }

        public Task<IList<VirtualMachineQueryEntity>> ListVirtualMachinesAsync(int limit, int offset)
        {
            throw new NotImplementedException();
        }
    }
}