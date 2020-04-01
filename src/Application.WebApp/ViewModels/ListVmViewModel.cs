using System.Collections.Generic;
using Application.Core.Dtos;
using Application.Core.Models;

namespace Application.WebApp.ViewModels
{
    public class ListVmViewModel
    {
        public IList<VirtualMachineDto> Vms { get; set; }
    }
}