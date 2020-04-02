using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Domain.Queries.VirtualMachine;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace Application.WebApi.Controllers
{
    using Domain.Dtos;
    
    [Route("api/[controller]")]
    [ApiController]
    public class VirtualMachinesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VirtualMachinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "GetAllVirtualMachines")]
        public async Task<ActionResult<List<VirtualMachineDto>>> GetAsync()
        {
            var response = await _mediator.Send(new GetAllVirtualMachinesQuery());

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

//        [HttpGet("{id}", Name = "GetVirtualMachine")]
//        public async Task<ActionResult<VirtualMachineDto>> GetAsync(
//            [FromRoute] Guid id)
//        {
//            var response = await _mediator.Send(new GetVirtualMachineRequest{
//                Id = id
//            });
//
//            if (response.HasError)
//            {
//                return BadRequest(response.Errors);
//            }
//
//            return Ok(response.Data);
//        }

//        [HttpPost(Name = "CreateVirtualMachine")]
//        public async Task<ActionResult<VirtualMachineDto>> CreateVirtualMachineAsync(
//            [FromBody] CreateVirtualMachineRequest request)
//        {
//            var response = await _mediator.Send(request);   
//
//            if (response.HasError)
//            {
//                return BadRequest(response.Errors);
//            }
//
//            var res = CreatedAtRoute(
//                "GetVirtualMachine", 
//                new {id = response.Data.Id}, 
//                response.Data);
//            
//            return res;
//        }

//        [HttpPut("{id}", Name = "UpdateVirtualMachine")]
//        public async Task<ActionResult> UpdateVirtualMachineAsync(
//            [FromBody] UpdateVirtualMachineRequest request,
//            [FromRoute] Guid id)
//        {
//            request.Id = id;
//            var response = await _mediator.Send(request);
//
//            if (response.HasError)
//            {
//                return BadRequest(response.Errors);
//            }
//
//            return Ok();
//        }

//        [HttpDelete("{id}", Name = "DeleteVirtualMachine")]
//        public async Task<ActionResult> DeleteVirtualMachineAsync(
//            [FromRoute] Guid id)
//        {
//            var response = await _mediator.Send(new DeleteVirtualMachineRequest
//            {
//                Id = id
//            });
//
//            if (response.HasError)
//            {
//                return NotFound();
//            }
//
//            return Ok();
//        }
    }
}