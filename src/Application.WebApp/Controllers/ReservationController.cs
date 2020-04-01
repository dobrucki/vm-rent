using System;
using System.Threading.Tasks;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApp.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly IMediator _mediator;

        public ReservationController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

        [HttpGet]
        public IActionResult CreateReservation([FromRoute] string id)
        {
            ViewBag.VmId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationViewModel viewModel)
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;
            var virtualMachineId = viewModel.VmId;
            try
            {
                await _mediator.Send(new CreateReservationRequest
                {
                    UserId = userId,
                    VirtualMachineId = Guid.Parse(virtualMachineId),
                    StartTime = viewModel.StartTime.Value,
                    EndTime = viewModel.EndTime
                });
            }
            catch (ArgumentException e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.VmId = viewModel.VmId;
                return View(viewModel);
            }

            return RedirectToAction("Details", "Vm", new {id = virtualMachineId});
        }

//        [HttpGet]
//        public IActionResult CancelReservation([FromRoute] string id)
//        {
//            var userVm = _reservationManager.GetReservationById(id);
//            _reservationManager.CancelReservationAsync(userVm.Result);
//            return RedirectToAction("Details", "User", new {id = userVm.Result.User.Id});
//        }
    }
}