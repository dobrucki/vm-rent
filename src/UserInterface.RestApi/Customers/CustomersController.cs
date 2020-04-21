using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Application.Customers;
using Core.Application.Customers.Commands.CreateCustomer;
using Core.Application.Customers.Commands.EditCustomerDetails;
using Core.Application.QueryModel.Customers.Queries;
using Core.Application.SharedKernel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserInterface.RestApi.Customers.CreateCustomer;
using UserInterface.RestApi.Customers.EditCustomerDetails;
using UserInterface.RestApi.Customers.ListCustomers;
using UserInterface.RestApi.SharedKernel;

namespace UserInterface.RestApi.Customers
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

        [HttpPost(Name = "CreateCustomer")]
        public async Task<ActionResult<CustomerDto>> PostAsync(
            [FromBody] CreateCustomerRequest request)
        {
            var command = new CreateCustomerCommand
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                EmailAddress = request.EmailAddress
            };
            await _mediator.Send(command);

            var customerQuery = new GetCustomerQuery
            {
                CustomerId = request.Id
            };
            var customer = await _mediator.Send(customerQuery);

            return CreatedAtRoute(
                "GetCustomer",
                new {id = customer.Id},
                customer);
        }
        
        [HttpGet(Name = "ListCustomers")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAsync([FromQuery] ListCustomersRequest request)
        {
            var query = new ListCustomersQuery
            {
                Limit = request.Limit,
                Offset = request.Offset
            };
            var rentals = await _mediator.Send(query);
            return Ok(rentals);
        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public async Task<ActionResult<CustomerDto>> GetAsync(
            [FromRoute] Guid id)
        {
            var query = new GetCustomerQuery
            {
                CustomerId = id
            };
            return Ok(await _mediator.Send(query));
        }

        [HttpPut("{id}", Name = "EditCustomerDetails")]
        public async Task<ActionResult> PutAsync(
            [FromRoute] Guid id,
            [FromBody] EditCustomerDetailsRequest request)
        {
            var command = new EditCustomerDetailsCommand
            {
                CustomerId = id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            await _mediator.Send(command);
            return Ok();
        }
    }
}