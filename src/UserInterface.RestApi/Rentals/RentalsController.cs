using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.CommandModel.Rentals.Commands;
using Core.Application.QueryModel.Rentals;
using Core.Application.QueryModel.Rentals.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserInterface.RestApi.Customers.ListCustomerRentals;
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
        public async Task<ActionResult<IEnumerable<RentalQueryEntity>>> GetAsync(
            [FromQuery] ListRentalsRequest request)
        {
            var query = new ListRentalsQuery
            {
                Limit = request.Limit,
                Offset = request.Offset
            };
            var rentals = await _mediator.Send(query);
            return Ok(rentals);
        }

        [HttpGet("{id}", Name = "GetRental")]
        public async Task<ActionResult<RentalQueryEntity>> GetAsync(
            [FromRoute] Guid id)
        {
            var query = new GetRentalQuery
            {
                RentalId = id
            };

            var rental = await _mediator.Send(query);
            return Ok(rental);
        }
        

        [HttpPost(Name = "CreateRental")]
        public async Task<ActionResult<RentalQueryEntity>> PostAsync(
            [FromBody] CreateRentalRequest request)
        {
            var command = new CreateRentalCommand
            {
                Id = request.Id,
                CustomerId = request.CustomerId,
                VirtualMachineId = request.VirtualMachineId,
                StartTime = request.StartTime,
                EndTime = request.EndTime
            };

            await _mediator.Send(command);
            
            var rentalQuery = new GetRentalQuery
            {
                RentalId = request.Id
            };
            var rental = await _mediator.Send(rentalQuery);

            return CreatedAtRoute(
                "GetRental",
                new {id = rental.Id},
                rental);
        }
    }
}