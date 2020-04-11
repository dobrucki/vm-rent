using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Rentals;
using Core.Application.Rentals.ListRentals;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
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
    }
}