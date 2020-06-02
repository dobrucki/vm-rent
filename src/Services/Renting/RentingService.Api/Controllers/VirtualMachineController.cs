using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingService.Application.Commands;

namespace RentingService.Api.Controllers
{
    [Route("api/virtualMachines")]
    [ApiController]
    public class VirtualMachineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VirtualMachineController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            var command = new CreateVirtualMachineCommand
            {
                Id = Guid.NewGuid(),
                Name = "Fedora"
            };
            await _mediator.Send(command);
            return Ok();
        }
    }
}