using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Application.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ReservationsController> _logger;

        public ReservationsController(
            IMediator mediator,
            ILogger<ReservationsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(Name = "ListAllReservations")]
        public async Task<ActionResult<List<UserDto>>> ListAllReservationsAsync()
        {
            var response = await _mediator.Send(new ListAllReservationsRequest());

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ActionResult<UserDto>> GetUserAsync(
            [FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetUserRequest
            {
                Id = id
            });

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }

        [HttpPost(Name = "CreateReservation")]
        public async Task<ActionResult<UserDto>> CreateReservationAsync(
            [FromBody] CreateReservationRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            var res = CreatedAtRoute(
                "GetUser",
                new {id = response.Data.Id},
                response.Data);

            return res;
        }

        [HttpPut("{id}", Name = "UpdateUser")]
        public async Task<ActionResult> UpdateUserAsync(
            [FromBody] UpdateUserRequest request,
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

        [HttpDelete("{id}", Name = "DeleteUser")]
        public async Task<ActionResult> DeleteUserAsync(
            [FromRoute] Guid id)
        {
            var response = await _mediator.Send(new DeleteUserRequest
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