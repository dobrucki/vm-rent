using System;
using System.Threading.Tasks;
using RentService.Core.Application.CommandModel.VirtualMachines;
using RentService.Core.Application.CommandModel.VirtualMachines.Commands;
using RentService.Core.Application.QueryModel.VirtualMachines;
using RentService.Core.Application.QueryModel.VirtualMachines.Queries;
using RentService.Core.Application.SharedKernel;
using RentService.Core.Application.SharedKernel.Exceptions;
using Core.Domain.VirtualMachines;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using RentService.Core.Application.Test.Mock;

namespace RentService.Core.Application.Test.Tests
{
    
    [TestFixture]
    public class VirtualMachineTest
    {
        private readonly IMediator _mediator;
        
        public VirtualMachineTest()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            
            var virtualMachineRepository = new MockVirtualMachineRepository();
            services.AddSingleton<IVirtualMachineRepository>(virtualMachineRepository);
            services.AddSingleton<IVirtualMachinesQueryRepository>(virtualMachineRepository);

            services.AddApplicationServices();
            var serviceProvider = services.BuildServiceProvider();
            _mediator = serviceProvider.GetRequiredService<IMediator>();
        }
        
        
        [Test]
        public async Task CreateVirtualMachine_ValidVirtualMachineProvided_VirtualMachineCreated()
        {
            var virtualMachine = new VirtualMachine
            {
                Id = Guid.NewGuid(),
                Name = $"Virtual machine {new Random().Next(1000, 9999)}"
            };
            await _mediator.Send(new CreateVirtualMachineCommand
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            });
            var virtualMachineEntity = await _mediator.Send(new GetVirtualMachineQuery
            {
                VirtualMachineId = virtualMachine.Id    
            });
            Assert.AreEqual(virtualMachine.Id.ToString(), virtualMachineEntity.Id);
            Assert.AreEqual(virtualMachine.Name, virtualMachineEntity.Name);
        }

        [Test]
        public async Task EditVirtualMachineDetails_DifferentDetails_FirstNameAndLastNameChanged()
        {
            var virtualMachine = new VirtualMachine
            {
                Id = Guid.NewGuid(),
                Name = $"Virtual machine {new Random().Next(1000, 9999)}"
            };
            await _mediator.Send(new CreateVirtualMachineCommand
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            });
            await _mediator.Send(new EditVirtualMachineDetailsCommand
            {
                VirtualMachineId = virtualMachine.Id,
                Name = virtualMachine.Name
            });
            var virtualMachineEntity = await _mediator.Send(new GetVirtualMachineQuery
            {
                VirtualMachineId = virtualMachine.Id
            });
            Assert.AreEqual(virtualMachine.Name, virtualMachineEntity.Name);
        }

        [Test]
        public async Task DeleteVirtualMachine_VirtualMachineExists_VirtualMachineRemovedFromItsRepository()
        {
            var virtualMachine = new VirtualMachine
            {
                Id = Guid.NewGuid(),
                Name = $"Virtual machine {new Random().Next(1000, 9999)}"
            };
            await _mediator.Send(new CreateVirtualMachineCommand
            {
                Id = virtualMachine.Id,
                Name = virtualMachine.Name
            });
            await _mediator.Send(new DeleteVirtualMachineCommand
            {
                Id = virtualMachine.Id
            });
            Assert.ThrowsAsync<NotFoundException>(() => _mediator.Send(new GetVirtualMachineQuery
            {
                VirtualMachineId = virtualMachine.Id
            }));
        }
    }
}