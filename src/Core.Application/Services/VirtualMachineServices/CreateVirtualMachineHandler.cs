using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Application.Interfaces;
using Core.Domain.Commands.VirtualMachineCommands;
using Core.Domain.Dtos;
using Core.Domain.Models.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Application.Services.VirtualMachineServices
{
    public class CreateVirtualMachineHandler :
        IRequestHandler<CreateVirtualMachineCommand, BaseResponseDto<VirtualMachineDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateVirtualMachineHandler> _logger;

        public CreateVirtualMachineHandler(
            IUnitOfWork unitOfWork, ILogger<CreateVirtualMachineHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<BaseResponseDto<VirtualMachineDto>> Handle(
            CreateVirtualMachineCommand request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<VirtualMachineDto>();
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    response.Errors.Add("Virtual machine name can not be null");
                }

                if (response.HasError) return response;
                
                var virtualMachine = new VirtualMachine
                {
                    Id = Guid.NewGuid(),
                    CreatedAt = DateTime.UtcNow,
                    ModifiedAt = null,

                    Name = request.Name
                };
                using (_unitOfWork)
                {
                    await _unitOfWork.VirtualMachines.InsertAsync(virtualMachine);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
            }

            return response;
        }
    }
}