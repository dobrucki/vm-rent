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
using Microsoft.Extensions.Logging;

namespace Application.Core.Services.VirtualMachineUseCases
{
    public class ListAllVirtualMachinesHandler :
        IRequestHandler<ListAllVirtualMachinesRequest, BaseResponseDto<List<VirtualMachineDto>>>
    {
        private readonly IRepository<VirtualMachine> _repository;
        private readonly ILogger<ListAllVirtualMachinesHandler> _logger;

        public ListAllVirtualMachinesHandler(
            IRepository<VirtualMachine> repository, 
            ILogger<ListAllVirtualMachinesHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<List<VirtualMachineDto>>> Handle(
            ListAllVirtualMachinesRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<List<VirtualMachineDto>>();

            try
            {
                var virtualMachines = (await _repository.GetAllAsync())
                    .Select(virtualMachine => new VirtualMachineDto
                        {
                            Id = virtualMachine.Id,
                            Name = virtualMachine.Name
                        })
                    .ToList();

                response.Data = virtualMachines;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occured while getting virtual machines.");
            }

            return response;
        }
    }
}