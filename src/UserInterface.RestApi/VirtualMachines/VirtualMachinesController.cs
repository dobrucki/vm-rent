using System;
using System.Threading.Tasks;
using Core.Application.VirtualMachines;
using Core.Application.VirtualMachines.GetVirtualMachine;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}