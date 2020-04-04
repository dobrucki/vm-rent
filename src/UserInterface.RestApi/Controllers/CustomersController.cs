using System;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Application.Customers.GetCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserInterface.RestApi.Requests.CustomerRequests;

namespace UserInterface.RestApi.Controllers
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

        // [HttpPost(Name = "CreateCustomer")]
        // public async Task<ActionResult<CustomerDto>> PostAsync(
        //     [FromBody] CreateCustomerRequest request)
        // {
        //     var command = new CreateCustomerCommand
        //     {
        //         FirstName = request.FirstName,
        //         LastName = request.LastName,
        //         EmailAddress = request.EmailAddress
        //     };
        //     var result = await _mediator.Send(command);
        //
        //     if (!result.Success)
        //     {
        //         return BadRequest(result.Errors);
        //     }
        //
        //     return CreatedAtRoute(
        //         "GetCustomer",
        //         new {id = result.Data.Id},
        //         result.Data);   
        // }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDto>> GetAsync(
            [FromRoute] Guid id)
        {
            var query = new GetCustomerQuery
            {
                CustomerId = id
            };
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}