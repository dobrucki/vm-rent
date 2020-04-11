using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Rentals;
using Core.Application.Rentals.CreateRental;
using Core.Application.Rentals.ListRentals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserInterface.RestApi.Rentals.CreateRental;
using UserInterface.RestApi.Rentals.ListRentals;

namespace UserInterface.RestApi.Rentals
{
    [ApiController]
    [Route("api/[controller]")]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(Name = "ListRentals")]
        public async Task<ActionResult<IEnumerable<RentalDto>>> GetAsync([FromQuery] ListRentalsRequest request)
        {
            var query = new ListRentalsQuery
            {
                Limit = request.Limit,
                Offset = request.Offset
            };
            var rentals = await _mediator.Send(query);
            return Ok(rentals);
        }

        [HttpPost(Name = "CreateRental")]
        public async Task<ActionResult<RentalDto>> PostAsync([FromBody] CreateRentalRequest request)
        {
            var command = new CreateRentalCommand
            {
                CustomerId = request.CustomerId,
                VirtualMachineId = request.VirtualMachineId,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            var rental = await _mediator.Send(command);
            // return CreatedAtRoute(
            //     "GetCustomer",
            //     new {id = rental.Id},
            //     rental);  
            return Ok(rental);
        }
    }
}