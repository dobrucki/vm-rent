using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.Core.Ports;
using MediatR;

namespace Application.Core.Services.VirtualMachineUseCases
{
    public class ListAllVirtualMachinesHandler :
        IRequestHandler<ListAllVirtualMachinesRequest, BaseResponseDto<List<VirtualMachineDto>>>
    {
        private readonly IRepository<VirtualMachine> _repository;

        public ListAllVirtualMachinesHandler(IRepository<VirtualMachine> repository)
        {
            _repository = repository;
        }

        public async Task<BaseResponseDto<List<VirtualMachineDto>>> Handle(
            ListAllVirtualMachinesRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<List<VirtualMachineDto>>();

            try
            {
                var virtualMachines =
                    (await _repository.GetAsync(e => true))
                    .Select(v => new VirtualMachineDto
                    {
                        Id = v.Id,
                        Name = v.Name
                    })
                    .ToList();

                response.Data = virtualMachines;
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occured while getting virtual machines.");
            }

            return response;
        }
    }
}