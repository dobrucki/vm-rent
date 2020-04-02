using System;
using System.Threading.Tasks;
using Application.Domain.Commands.CustomerCommands;
using Application.Domain.Dtos;
using Application.Domain.Queries.CustomerQueries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApi.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost(Name = "AddCustomer")]
        public async Task<ActionResult<CustomerDto>> PostAsync(
            [FromBody] CreateCustomerCommand request)
        {
            var response = await _mediator.Send(request);   

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            var result = CreatedAtRoute(
                "GetCustomer", 
                new {id = response.Data.Id}, 
                response.Data);

            return result;
        }
        
        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDto>> GetAsync(
            [FromRoute] Guid id)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery {
                CustomerId = id
            });

            if (response.HasError)
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Data);
        }
    }
}