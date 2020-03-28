using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.Core.Ports;

namespace Application.Infrastructure.InMemoryDataAccess
{
    public sealed class VirtualMachineRepository : IRepository<VirtualMachine>
    {
        private readonly DataContext _context;
        
        public VirtualMachineRepository()
        {
            _context = DataContext.Context;
        }

        public async Task<VirtualMachine> GetAsync(Guid id)
        {
            var entity = await Task.Run(() => _context.VirtualMachines[id]);
            return new VirtualMachine
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public async Task<IEnumerable<VirtualMachine>> GetAllAsync()
        {
            var entities = new List<VirtualMachine>();
            await Task.Run(() => 
                _context.VirtualMachines.Values
                    .ToImmutableList()
                    .ForEach(entity => entities.Add(entity))
                );
            return entities;
        }

        public async Task<IEnumerable<VirtualMachine>> GetAllAsync(Expression<Func<VirtualMachine, bool>> predicate)
        {
            var entities = new List<VirtualMachine>();
            await Task.Run(() => 
                _context.VirtualMachines.Values
                    .Where(predicate.Compile())
                    .ToImmutableList()
                    .ForEach(entity => entities.Add(entity))
            );
            return entities;
        }

        public async Task CreateAsync(VirtualMachine virtualMachine)
        {
            await Task.Run(() => _context.VirtualMachines.Add(virtualMachine.Id, virtualMachine));
        }

        public async Task UpdateAsync(VirtualMachine virtualMachine)
        {
            var entity = _context.VirtualMachines[virtualMachine.Id];
            await Task.Run(() => { entity.Name = virtualMachine.Name; });
        }

        public async Task DeleteAsync(VirtualMachine virtualMachine)
        {
            await Task.Run(() => _context.VirtualMachines.Remove(virtualMachine.Id));
        }
    }
}