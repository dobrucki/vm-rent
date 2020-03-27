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
    public class GetVirtualMachineHandler :
        IRequestHandler<GetVirtualMachineRequest, BaseResponseDto<VirtualMachineDto>>
    {
        private readonly IRepository<VirtualMachine> _repository;
        private readonly ILogger<GetVirtualMachineHandler> _logger;

        public GetVirtualMachineHandler(
            IRepository<VirtualMachine> repository, 
            ILogger<GetVirtualMachineHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<VirtualMachineDto>> Handle(
            GetVirtualMachineRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<VirtualMachineDto>();

            try
            {
                var virtualMachine = await _repository.GetAsync(request.Id);

                response.Data = new VirtualMachineDto
                {
                    Id = virtualMachine.Id,
                    Name = virtualMachine.Name
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occured while getting virtual machine.");
            }

            return response;
        }
    }
}