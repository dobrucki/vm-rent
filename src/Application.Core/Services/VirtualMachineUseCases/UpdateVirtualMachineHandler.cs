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
        private IRepository<VirtualMachine> _repository;
        private ILogger<UpdateVirtualMachineHandler> _logger;

        public UpdateVirtualMachineHandler(
            IRepository<VirtualMachine> repository, 
            ILogger<UpdateVirtualMachineHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseDto<bool>> Handle(
            UpdateVirtualMachineRequest request,
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                var virtualMachine = new VirtualMachine
                {
                    Id = request.Id,
                    Name = request.Name
                };
                await _repository.UpdateAsync(virtualMachine);
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