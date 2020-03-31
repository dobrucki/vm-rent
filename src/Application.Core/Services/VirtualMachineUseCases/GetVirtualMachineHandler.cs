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
        private readonly ILogger<GetVirtualMachineHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public GetVirtualMachineHandler(
            ILogger<GetVirtualMachineHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseDto<VirtualMachineDto>> Handle(
            GetVirtualMachineRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<VirtualMachineDto>();

            try
            {
                using (_unitOfWork)
                {
                    var virtualMachine = await _unitOfWork.VirtualMachines.GetByIdAsync(request.Id);
                    response.Data = new VirtualMachineDto
                    {
                        Id = virtualMachine.Id,
                        Name = virtualMachine.Name
                    };
                    _unitOfWork.Complete();
                    _logger.LogInformation($"Updated virtual machine with id {request.Id}");
                }
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