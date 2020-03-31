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
    public class CreateVirtualMachineHandler : 
        IRequestHandler<CreateVirtualMachineRequest, BaseResponseDto<VirtualMachineDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateVirtualMachineHandler> _logger;

        public CreateVirtualMachineHandler(
            ILogger<CreateVirtualMachineHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<BaseResponseDto<VirtualMachineDto>> Handle(
            CreateVirtualMachineRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<VirtualMachineDto>();

            try
            {
                using (_unitOfWork)
                {
                    var virtualMachine = new VirtualMachine
                    {
                        Id = Guid.NewGuid(),
                        Name = request.Name
                    };
                    await _unitOfWork.VirtualMachines.InsertAsync(virtualMachine);
                    response.Data = new VirtualMachineDto
                    {
                        Id = virtualMachine.Id,
                        Name = virtualMachine.Name
                    };
                    _unitOfWork.Complete();
                    _logger.LogInformation($"Created virtual machine with id {virtualMachine.Id}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response.Errors.Add("An error occurred while creating the virtual machine.");
            }

            return response;
        }
    }
}