using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.WebApp.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Application.WebApp.Controllers
{
    public class VmController : Controller
    {

        private readonly HttpClient _httpClient = new HttpClient();
        private readonly IMediator _mediator;

        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        
        private readonly Uri _baseAddress = new Uri("https://localhost:5001");

        public VmController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<IActionResult> All(string name)
        {
            var q = "https://localhost:5001/api/vm";
            if (name != null)
            {
                q += $"?name={name}";
            }

            var response = await _httpClient.GetAsync(q);
            var json = await response.Content.ReadAsStringAsync();
            var viewModel = new ListVmViewModel
            {
                Vms = JsonConvert.DeserializeObject<IList<VirtualMachineDto>>(json, _jsonSerializerSettings)
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator,Employee")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Employee")]
        public async Task<IActionResult> Create(CreateVmViewModel viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = cookieContainer})
            using (var client = new HttpClient(handler) {BaseAddress = _baseAddress})
            {
                var content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("name", viewModel.Name),
                    new KeyValuePair<string, string>("type", viewModel.Type),
                    new KeyValuePair<string, string>("comment", viewModel.Comment) 
                });
                var result = await client.PostAsync("/api/vm", content);
                try
                {
                    result.EnsureSuccessStatusCode();
                    return RedirectToAction("All", "Vm");
                }
                catch (HttpRequestException)
                {
                    ModelState.AddModelError("", "Cannot create vm");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                return View(viewModel);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> Details([FromRoute] string id)
        {
            var vm = _mediator.Send(new GetVirtualMachineRequest
            {
                Id = Guid.Parse(id)
            });
            return View(new DetailVmViewModel{
                UserVms = new List<Reservation>()
            });
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Edit([FromRoute] string id)
        {
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = cookieContainer})
            using (var client = new HttpClient(handler) {BaseAddress = _baseAddress})
            {
//                var content = new FormUrlEncodedContent(new []
//                {
//                    new KeyValuePair<string, string>("name", viewModel.Name),
//                    new KeyValuePair<string, string>("type", viewModel.Type),
//                    new KeyValuePair<string, string>("comment", viewModel.Comment) 
//                });
                var result = await client.GetAsync($"/api/vm/{id}");
                var vm = JsonConvert
                    .DeserializeObject<VirtualMachineDto>(await result.Content.ReadAsStringAsync(), _jsonSerializerSettings);
                return View(new EditVmViewModel
                {
                    Id = vm.Id.ToString(),
                    Name = vm.Name
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Edit(EditVmViewModel viewModel)
        {
//            var vm = _vmManager.GetVmById(viewModel.Id).Result;
//            vm.Name = viewModel.Name;
//            if (vm.GetType() == typeof(ExtendedVm))
//            {
//                ((ExtendedVm) vm).Comment = viewModel.Comment;
//            }
//
//            try
//            {
//                _vmManager.UpdateVm(vm);
//            }
//            catch (ArgumentException e)
//            {
//                ModelState.AddModelError("", e.Message);
//                return View(viewModel);
//            }
//
//            return RedirectToAction("All", "Vm");
            if (!ModelState.IsValid) return View(viewModel);
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = cookieContainer})
            using (var client = new HttpClient(handler) {BaseAddress = _baseAddress})
            {
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("id", viewModel.Id),
                    new KeyValuePair<string, string>("name", viewModel.Name),
                    new KeyValuePair<string, string>("comment", viewModel.Comment)
                });
                var result = await client.PutAsync($"/api/vm/{viewModel.Id}", content);
                //var vm = JsonConvert.DeserializeObject<Vm>(await result.Content.ReadAsStringAsync());
                if (result.StatusCode == HttpStatusCode.NoContent) return RedirectToAction("All");
                ModelState.AddModelError("error", $"Cannot edit");
                return View(viewModel);

            }

            return RedirectToAction("All");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator, Employee")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
//            var vm = _vmManager.GetVmById(id).Result;
//            _vmManager.DeleteVm(vm);
//            return RedirectToAction("All", "Vm");

            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler {CookieContainer = cookieContainer})
            using (var client = new HttpClient(handler) {BaseAddress = _baseAddress})
            {
                var result = await client.DeleteAsync($"/api/vm/{id}");
                var vm = JsonConvert
                    .DeserializeObject<VirtualMachineDto>(await result.Content.ReadAsStringAsync(), _jsonSerializerSettings);
                try
                {
                    result.EnsureSuccessStatusCode();
                }
                catch(HttpRequestException)
                {
                    ModelState.AddModelError("error", result.ReasonPhrase);
                    return BadRequest(ModelState);
                }
                return RedirectToAction("All", "Vm");
            }
        }
    }
}