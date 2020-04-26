using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.CommandModel;
using Core.Application.CommandModel.Rentals.Events;
using Core.Application.QueryModel.Rentals;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Query.Rentals
{
    public class RentalRepository : IRentalsQueryRepository,
        IEventHandler<RentalCreatedEvent>
    {
        private readonly IMongoCollection<RentalEntity> _rentals;
        private readonly ILogger<RentalRepository> _logger;

        public RentalRepository(IMongoClient client, ILogger<RentalRepository> logger)
        {
            _logger = logger;
            var database = client.GetDatabase("vm_rent");
            _rentals = database.GetCollection<RentalEntity>("Rentals");
        }
        
        public async Task<List<RentalQueryEntity>> ListRentalsAsync(int limit, int offset)
        {
            var rentals = await _rentals
                .Find(_ => true)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return rentals.Select(x => new RentalQueryEntity
            {
                Id = x.Id,
                VirtualMachineId = x.VirtualMachineId,
                CustomerId = x.CustomerId,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();
        }

        public async Task<List<RentalQueryEntity>> ListCustomerRentalsAsync(int limit, int offset, Guid customerId)
        {
            var filter = Builders<RentalEntity>.Filter
                .Eq(x => x.CustomerId, customerId.ToString());
            var rentals = await _rentals
                .Find(filter)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return rentals.Select(x => new RentalQueryEntity
            {
                Id = x.Id,
                VirtualMachineId = x.VirtualMachineId,
                CustomerId = x.CustomerId,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();
        }

        public async Task<List<RentalQueryEntity>> ListVirtualMachineRentalsAsync(
            int limit, int offset, Guid virtualMachineId)
        {
            var filter = Builders<RentalEntity>.Filter
                .Eq(x => x.VirtualMachineId, virtualMachineId.ToString());
            var rentals = await _rentals
                .Find(filter)
                .Skip(offset * limit)
                .Limit(limit)
                .ToListAsync();
            return rentals.Select(x => new RentalQueryEntity
            {
                Id = x.Id,
                VirtualMachineId = x.VirtualMachineId,
                CustomerId = x.CustomerId,
                StartTime = x.StartTime,
                EndTime = x.EndTime
            }).ToList();
        }

        public async Task<RentalQueryEntity> GetRentalByIdAsync(string id)
        {
            var rental = await _rentals
                .Find(x => x.Id.Equals(id))
                .SingleOrDefaultAsync();
            return new RentalQueryEntity
            {
                Id = rental.Id,
                VirtualMachineId = rental.VirtualMachineId,
                CustomerId = rental.CustomerId,
                StartTime = rental.StartTime,
                EndTime = rental.EndTime
            };  
        }

        public async Task Handle(RentalCreatedEvent notification, CancellationToken cancellationToken)
        {
            var rentalEntity = new RentalEntity
            {
                Id = notification.Rental.Id.ToString(),
                VirtualMachineId = notification.Rental.VirtualMachine.Id.ToString(),
                CustomerId = notification.Rental.Customer.Id.ToString(),
                EndTime = notification.Rental.EndTime.ToString(CultureInfo.InvariantCulture),
                StartTime = notification.Rental.StartTime.ToString(CultureInfo.InvariantCulture)
            };
            await _rentals.InsertOneAsync(rentalEntity, cancellationToken);
        }
        
    }
}