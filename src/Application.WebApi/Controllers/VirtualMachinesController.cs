using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualMachinesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<VirtualMachinesController> _logger;

        public VirtualMachinesController(
            IMediator mediator, 
            ILogger<VirtualMachinesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "ListVirtualMachines")]
        public async Task<ActionResult<List<VirtualMachineDto>>> ListAllVirtualMachinesAsync()
        {
            var response = await _mediator.Send(new ListAllVirtualMachinesRequest());

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

        [HttpGet("{id}", Name = "GetVirtualMachine")]
        public async Task<ActionResult<VirtualMachineDto>> GetVirtualMachineAsync(
            [FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetVirtualMachineRequest{
                Id = id
            });

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

        [HttpPost(Name = "CreateVirtualMachine")]
        public async Task<ActionResult<VirtualMachineDto>> CreateVirtualMachineAsync(
            [FromBody] CreateVirtualMachineRequest request)
        {
            var response = await _mediator.Send(request);   

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            var res = CreatedAtRoute(
                "GetVirtualMachine", 
                new {id = response.Data.Id}, 
                response.Data);
            
            return res;
        }

        [HttpPut("{id}", Name = "UpdateVirtualMachine")]
        public async Task<ActionResult> UpdateVirtualMachineAsync(
            [FromBody] UpdateVirtualMachineRequest request,
            [FromRoute] Guid id)
        {
            request.Id = id;
            var response = await _mediator.Send(request);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        [HttpDelete("{id}", Name = "DeleteVirtualMachine")]
        public async Task<ActionResult> DeleteVirtualMachineAsync(
            [FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteVirtualMachineRequest
            {
                Id = id
            });

            if (response.HasError)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}