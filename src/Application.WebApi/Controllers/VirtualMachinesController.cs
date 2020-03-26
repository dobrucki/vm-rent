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
            var createResponse = await _mediator.Send(new ListAllVirtualMachinesRequest());

            if (createResponse.HasError)
            {
                return BadRequest(createResponse.Errors);
            }

            return Ok(createResponse.Data);
        }

        [HttpPost]
        public async Task<ActionResult<string>> CreateVirtualMachineAsync(
            [FromBody] CreateVirtualMachineRequest request)
        {
            var createResponse = await _mediator.Send(request);

            if (createResponse.Data)
            {
                return Created("...", null);
            }
            else
            {
                return BadRequest(createResponse.Errors);
            }
        }
    }
}