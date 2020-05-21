using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingService.Application.Commands;

namespace RentingService.Api.Controllers
{
    [Route("api/rentals")]
    [ApiController]
    public class RentalController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }
        
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            var command = new CreateRentalCommand(Guid.NewGuid(), Guid.NewGuid(), "Mirek", 
                "Kudra", "mirek.kudra@example.com", 
                Guid.NewGuid(), DateTime.Now);
            await _mediator.Send(command);
            return Ok();
        }
    }
}