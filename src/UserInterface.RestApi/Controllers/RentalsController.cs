using System;
using System.Threading.Tasks;
using Core.Application.Dtos;
using Core.Domain.Commands.RentalCommands;
using Core.Domain.Queries.RentalQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UserInterface.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RentalsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost(Name = "AddRental")]
        public async Task<ActionResult<CustomerDto>> PostAsync(
            [FromBody] CreateRentalCommand request)
        {
            var response = await _mediator.Send(request);   

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            var result = CreatedAtRoute(
                "GetRental", 
                new {id = response.Data.Id}, 
                response.Data);

            return result;
        }

        [HttpGet(Name = "GetRental")]
        public async Task<ActionResult<RentalDto>> GetAsync(
            [FromBody] GetRentalByIdQuery request,
            [FromRoute] Guid rentalId)
        {
            request.Id = rentalId;
            var response = await _mediator.Send(request);
            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }
    }
}