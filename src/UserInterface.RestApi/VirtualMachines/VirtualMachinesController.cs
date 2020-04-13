using System;
using System.Threading.Tasks;
using Core.Application.VirtualMachines;
using Core.Application.VirtualMachines.CreateVirtualMachine;
using Core.Application.VirtualMachines.DeleteVirtualMachine;
using Core.Application.VirtualMachines.EditVirtualMachineDetails;
using Core.Application.VirtualMachines.GetVirtualMachine;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserInterface.RestApi.VirtualMachines.CreateVirtualMachine;
using UserInterface.RestApi.VirtualMachines.EditVirtualMachineDetails;

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
        public async Task<ActionResult<VirtualMachineDto>> GetAsync([FromRoute] Guid id)
        {
            var query = new GetVirtualMachineQuery
            {
                VirtualMachineId = id
            };
            return Ok(await _mediator.Send(query));
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
        public async Task<ActionResult<VirtualMachineDto>> PostAsync([FromBody] CreateVirtualMachineRequest request)
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
    }
}