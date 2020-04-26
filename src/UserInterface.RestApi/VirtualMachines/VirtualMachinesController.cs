using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.CommandModel.VirtualMachines.Commands;
using Core.Application.QueryModel.Rentals;
using Core.Application.QueryModel.Rentals.Queries;
using Core.Application.QueryModel.VirtualMachines;
using Core.Application.QueryModel.VirtualMachines.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserInterface.RestApi.Customers.ListCustomerRentals;
using UserInterface.RestApi.VirtualMachines.CreateVirtualMachine;
using UserInterface.RestApi.VirtualMachines.EditVirtualMachineDetails;
using UserInterface.RestApi.VirtualMachines.ListVirtualMachines;

namespace UserInterface.RestApi.VirtualMachines
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class VirtualMachinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VirtualMachinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}", Name = "GetVirtualMachine")]
        public async Task<ActionResult<VirtualMachineQueryEntity>> GetAsync([FromRoute] Guid id)
        {
            var query = new GetVirtualMachineQuery
            {
                VirtualMachineId = id
            };
            return Ok(await _mediator.Send(query));
        }

        public async Task<ActionResult<IEnumerable<VirtualMachineQueryEntity>>> GetAsync(
            [FromQuery] ListVirtualMachinesRequest request)
        {
            var query = new ListVirtualMachinesQuery
            {
                Limit = request.Limit,
                Offset = request.Offset
            };
            var virtualMachines = await _mediator.Send(query);
            return Ok(virtualMachines);
        }

        [HttpPut("{id}", Name = "EditVirtualMachineDetails")]
        public async Task<ActionResult> PutAsync([FromRoute] Guid id, [FromBody] EditVirtualMachineDetailsRequest request)
        {
            var command = new EditVirtualMachineDetailsCommand
            {
                VirtualMachineId = id,
                Name = request.Name
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPost(Name = "CreateVirtualMachine")]
        public async Task<ActionResult<VirtualMachineQueryEntity>> PostAsync([FromBody] CreateVirtualMachineRequest request)
        {
            var command = new CreateVirtualMachineCommand
            {
                Id = request.Id,
                Name = request.Name
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteVirtualMachine")]
        public async Task<ActionResult> DeleteAsync([FromRoute] Guid id)
        {
            var command = new DeleteVirtualMachineCommand
            {
                Id = id
            };
            await _mediator.Send(command);
            return Ok();
        }
        
        [HttpGet("{virtualMachineId}/Rentals", Name = "ListVirtualMachineRentals")]
        public async Task<ActionResult<IEnumerable<RentalQueryEntity>>> GetAsync(
            [FromQuery] ListCustomerRentalsRequest request,
            [FromRoute] Guid virtualMachineId)
        {
            var query = new ListVirtualMachineRentalsQuery
            {
                Limit = request.Limit,
                Offset = request.Offset,
                VirtualMachineId = virtualMachineId
            };
            var rentals = await _mediator.Send(query);
            return Ok(rentals);
        }
    }
}