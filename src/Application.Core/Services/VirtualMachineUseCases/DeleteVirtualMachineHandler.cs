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
    public class DeleteVirtualMachineHandler :
        IRequestHandler<DeleteVirtualMachineRequest, BaseResponseDto<bool>>
    {
        private ILogger<DeleteVirtualMachineHandler> _logger;
        private IUnitOfWork _unitOfWork;

        public DeleteVirtualMachineHandler(
            ILogger<DeleteVirtualMachineHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BaseResponseDto<bool>> Handle(
            DeleteVirtualMachineRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                await using (_unitOfWork)
                {
                    var virtualMachine = await _unitOfWork.VirtualMachines.GetAsync(request.Id);
                    await _unitOfWork.VirtualMachines.RemoveAsync(virtualMachine);
                    await _unitOfWork.Complete();
                    _logger.LogInformation($"Removed virtual machine with id {request.Id}");
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