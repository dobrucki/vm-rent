using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApp.Controllers
{
    [Route("api/vm")]
    [AllowAnonymous]
    public class RestVmController : Controller
    {
        private readonly IMediator _mediator;

        public RestVmController(IMediator mediator)
        {
            _mediator = mediator;
        }


        // GET: api/vm/
        // GET: api/vm?name=machine
        [HttpGet]
        public async Task<IActionResult> Search(string name)
        {
            var request = new ListAllVirtualMachinesRequest();
            var vms = await _mediator.Send(request);
            if (vms.HasError) return BadRequest(vms.Errors);
            var viewModels = new ListVmViewModel();
            if (name != null)
            {
                viewModels.Vms = vms.Data.Where(vm => vm.Name.Contains(name)).ToList();
            }
            return Json(vms);
        }

        // GET: api/vm/c5f41682-de9a-4532-83fb-081aa338bdb8
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _mediator.Send(new GetVirtualMachineRequest
            {
                Id = Guid.Parse(id)
            });
            if (response.HasError) return NotFound();
            return Json(response.Data);
        }

        // POST: api/vm/
        [HttpPost]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Post(CreateVmViewModel viewModel)
        {
            try
            {
                VirtualMachineDto vm;
                    vm = (await _mediator.Send(new CreateVirtualMachineRequest
                    {
                        Name = viewModel.Name
                    })).Data;
                

                return CreatedAtAction("Get", new {id = vm.Id}, vm);
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("error", e.Message);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/vm/c5f41682-de9a-4532-83fb-081aa338bdb8
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Put(string id, EditVmViewModel viewModel)
        {
            try
            {
                await _mediator.Send(new UpdateVirtualMachineRequest
                {
                    Id = Guid.Parse(id),
                    Name = viewModel.Name
                });
                return NoContent();
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("error", e.Message);
            }

            return BadRequest(ModelState);
        }
        
        // DELETE: api/vm/c5f41682-de9a-4532-83fb-081aa338bdb8
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _mediator.Send(new DeleteVirtualMachineRequest
                {
                    Id = Guid.Parse(id)
                });
                return NoContent();
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("error", e.Message);
            }

            return BadRequest(ModelState);
        }
    }
}