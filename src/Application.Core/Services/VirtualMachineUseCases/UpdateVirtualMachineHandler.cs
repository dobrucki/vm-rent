using System;
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
    public class UpdateVirtualMachineHandler :
        IRequestHandler<UpdateVirtualMachineRequest, BaseResponseDto<bool>>
    {
        private readonly ILogger<UpdateVirtualMachineHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateVirtualMachineHandler(
            ILogger<UpdateVirtualMachineHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseDto<bool>> Handle(
            UpdateVirtualMachineRequest request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                using (_unitOfWork)
                {
                    var virtualMachine = await _unitOfWork.VirtualMachines.GetByIdAsync(request.Id);
                    virtualMachine.Name = request.Name;
                    await _unitOfWork.VirtualMachines.UpdateAsync(virtualMachine);
                    _unitOfWork.Complete();
                    _logger.LogInformation($"Updated virtual machine with id {request.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add(ex.Message);
            }

            return response;
        }
    }
}