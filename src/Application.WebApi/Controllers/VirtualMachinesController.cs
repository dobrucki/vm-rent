using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Application.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualMachinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VirtualMachinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<VirtualMachineDto>>> ListAllVirtualMachines()
        {
            var response = await _mediator.Send(new ListAllVirtualMachinesRequest());

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VirtualMachineDto>> GetVirtualMachine(
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

        [HttpPost]
        public async Task<ActionResult<string>> CreateVirtualMachineAsync(
            [FromBody] CreateVirtualMachineRequest request)
        {
            var response = await _mediator.Send(request);   

            if (response.Data)
            {
                return Created("...", null);
            }
            else
            {
                return BadRequest(response.Errors);
            }
        }

        [HttpPut("{id}")]
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
    }
}