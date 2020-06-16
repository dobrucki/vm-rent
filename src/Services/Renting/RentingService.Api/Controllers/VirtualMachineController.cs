using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RentingService.Api.Requests;
using RentingService.Api.ViewModels;
using RentingService.Application.Commands;
using RentingService.Application.Queries;

namespace RentingService.Api.Controllers
{
    [Route("api/virtualMachines")]
    [ApiController]
    public class VirtualMachineController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly VirtualMachineQueries _virtualMachineQueries;

        public VirtualMachineController(IMediator mediator, VirtualMachineQueries virtualMachineQueries, IMapper mapper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _virtualMachineQueries = virtualMachineQueries
                                     ?? throw new ArgumentNullException(nameof(virtualMachineQueries));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VirtualMachineViewModel>>> GetAsync(
            GetVirtualMachinesRequest request)
        {
            var virtualMachines = _mapper
                .Map<IList<VirtualMachineViewModel>>(await _virtualMachineQueries
                    .GetVirtualMachines(request.Limit, request.Page));
            return Ok(virtualMachines);
        }
        
        [HttpPost]
        public async Task<ActionResult> PostAsync()
        {
            var command = new CreateVirtualMachineCommand
            {
                Id = Guid.NewGuid(),
                Name = "Fedora"
            };
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> PutAsync()
        {
            var command = new EditVirtualMachineNameCommand
            {
                Id = Guid.Parse("684848a8-0f27-4b12-8c11-ad80c02257ac"),
                Name = "Debian"
            };
            await _mediator.Send(command);
            return Ok();
        }
    }
}