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
        private IRepository<VirtualMachine> _repository;
        private ILogger<DeleteVirtualMachineHandler> _logger;

        public DeleteVirtualMachineHandler(
            IRepository<VirtualMachine> repository, 
            ILogger<DeleteVirtualMachineHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        
        public async Task<BaseResponseDto<bool>> Handle(
            DeleteVirtualMachineRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                await _repository.DeleteAsync(request.Id);
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