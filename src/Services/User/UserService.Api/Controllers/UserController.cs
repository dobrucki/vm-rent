using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands;
using UserService.Domain.Models.UserAggregate;

namespace UserService.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public ActionResult GetTest()
        {
            return Accepted();
        }
        
        // TODO: Change return type from User to it's dto type
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            var command = new RegisterUserCommand(
                Guid.NewGuid(), "test", "Test", "Test",
                "test@example.com", "test");
            await _mediator.Send(command);
            return Ok();
        }
    }
}