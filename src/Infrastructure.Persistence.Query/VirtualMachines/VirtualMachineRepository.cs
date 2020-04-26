using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.CommandModel;
using Core.Application.CommandModel.VirtualMachines.Events;
using Core.Application.QueryModel.VirtualMachines;
using Infrastructure.Persistence.Query.Rentals;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Query.VirtualMachines
{
    public class VirtualMachineRepository : IVirtualMachinesQueryRepository,
        IEventHandler<VirtualMachineCreatedEvent>,
        IEventHandler<VirtualMachineDetailsEditedEvent>,
        IEventHandler<VirtualMachineDeletedEvent>
    {
        private readonly IMongoCollection<VirtualMachineEntity> _virtualMachines;

        public VirtualMachineRepository(IMongoClient client)
        {
            var database = client.GetDatabase("vm_rent");
            _virtualMachines = database.GetCollection<VirtualMachineEntity>("VirtualMachines");
        }
        public async Task<VirtualMachineQueryEntity> GetVirtualMachineByIdAsync(Guid virtualMachineId)
        {
            var virtualMachine = await _virtualMachines
                .Find(x => x.Id.Equals(virtualMachineId.ToString()))
                .SingleOrDefaultAsync();
            return new VirtualMachineQueryEntity
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            };    
        }

        public async Task<IList<VirtualMachineQueryEntity>> ListVirtualMachinesAsync(int limit, int offset)
        {
            var virtualMachines = await _virtualMachines
                .Find(_ => true)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return virtualMachines.Select(x => new VirtualMachineQueryEntity
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public async Task Handle(VirtualMachineCreatedEvent notification, CancellationToken cancellationToken)
        {
            var customerEntity = new VirtualMachineEntity
            {
                Id = notification.VirtualMachine.Id.ToString(),
                Name = notification.VirtualMachine.Name,
            };
            await _virtualMachines.InsertOneAsync(customerEntity, cancellationToken);
        }

        public async Task Handle(VirtualMachineDetailsEditedEvent notification, CancellationToken cancellationToken)
        {
            var filter = Builders<VirtualMachineEntity>.Filter
                .Eq(x => x.Id, notification.VirtualMachine.Id.ToString());

            var update = Builders<VirtualMachineEntity>.Update
                .Set(x => x.Name, notification.VirtualMachine.Name);

            await _virtualMachines.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
        }

        public async Task Handle(VirtualMachineDeletedEvent notification, CancellationToken cancellationToken)
        {
            var virtualMachineFilter = Builders<VirtualMachineEntity>.Filter
                .Eq(x => x.Id, notification.VirtualMachine.Id.ToString());
            await _virtualMachines.DeleteOneAsync(virtualMachineFilter, cancellationToken);
        }
    }
}