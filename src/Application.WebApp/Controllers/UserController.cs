using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Core.Models;
using Application.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.WebApp.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;

        private readonly IMediator _mediator;

        public UserController(UserManager<User> userManager, IMediator mediator)
        {
            _userManager = userManager;
            _mediator = mediator;
        }

//        [HttpGet]
//        public IActionResult All()
//        {
//            var viewModel = new ListUserViewModel
//            {
//                Users = _userManager.Users.ToList()
//            };
//            return View(viewModel);
//        }
        
        [HttpGet]
        public IActionResult All(string name)
        {
            var viewModel = new ListUserViewModel();
            if (string.IsNullOrWhiteSpace(name))
            {
                viewModel.Users = _userManager.Users.ToList();
            }
            else
            {
                viewModel.Users = _userManager.Users
                    .Where(u => u.Username.Contains(name))
                    .ToList();
            }
            return View(viewModel);
        }

//        [HttpGet]
//        public IActionResult Details(string id)
//        {
//            var user = _userManager.Users.FirstOrDefault(u => string.Equals(u.Id.ToString(), id));
//            return View(new DetailUserViewModel
//            {
//                User = user,
//                UserVms = _reservationManager.GetReservationsForUserAsync(user).Result
//            });
//        }

//        [HttpGet]
//        public async Task<IActionResult> Edit(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user is null) return NotFound();
//            return View(new EditUserViewModel
//            {
//                UserId = user.Id,
//                UserName = user.UserName,
//                Email = user.Email,
//                PhoneNumber = user.PhoneNumber,
//                IsActive = user.Active
//            });
//        }
//
//        [HttpPost]
//        public async Task<IActionResult> Edit(EditUserViewModel viewModel)
//        {
//            if (!ModelState.IsValid) return View(viewModel);
//            //var user = _userManager.Users.FirstOrDefault(u => string.Equals(u.Id, viewModel.UserId));
//            var user = await _userManager.FindByIdAsync(viewModel.UserId);
//            if (user is null) return NotFound();
//            user.Email = viewModel.Email;
//            user.UserName = viewModel.UserName;
//            user.PhoneNumber = viewModel.PhoneNumber;
//            user.Active = viewModel.IsActive;
//            await _userManager.UpdateAsync(user);
//            return RedirectToAction("All");
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> AddToRole(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user is null) return NotFound();
//            return View(new ModifyUserRolesViewModel
//            {
//                UserId = user.Id,
//                UserRoles = await _userManager.GetRolesAsync(user)
//            });
//        }
//
//        [HttpPost]
//        public async Task<IActionResult> AddToRole(ModifyUserRolesViewModel viewModel)
//        {
//            if (!ModelState.IsValid) return View(viewModel);
//            var user = await _userManager.FindByIdAsync(viewModel.UserId);
//            if (user is null) return NotFound();
//            await _userManager.AddToRoleAsync(user, viewModel.Role);
//            return RedirectToAction("All");
//        }
//
//        [HttpGet]
//        public async Task<IActionResult> RemoveFromRole(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            if (user is null) return NotFound();
//            return View(new ModifyUserRolesViewModel
//            {
//                UserId = user.Id,
//                UserRoles = await _userManager.GetRolesAsync(user)
//            });
//        }
//
//        [HttpPost]
//        public async Task<IActionResult> RemoveFromRole(ModifyUserRolesViewModel viewModel)
//        {
//            if (!ModelState.IsValid) return View(viewModel);
//            var user = await _userManager.FindByIdAsync(viewModel.UserId);
//            if (user is null) return NotFound();
//            await _userManager.RemoveFromRoleAsync(user, viewModel.Role);
//            return RedirectToAction("All");
//        }
//
//        [HttpGet]
//        [AllowAnonymous]
//        JsonResult IsUsernameInUse(string UserName)
//        {
//            return Json(!_userManager.Users
//                .Any(u => string.Equals(u.UserName, UserName, StringComparison.CurrentCultureIgnoreCase)));
//        }
    }
}

