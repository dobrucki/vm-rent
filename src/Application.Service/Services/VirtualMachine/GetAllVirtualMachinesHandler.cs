using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Domain.Dtos;
using Application.Service.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Service.Services.VirtualMachine
{
    using Domain.Queries.VirtualMachine;
    
    public class GetAllVirtualMachinesHandler :
        IRequestHandler<GetAllVirtualMachinesQuery, BaseResponseDto<IEnumerable<VirtualMachineDto>>>
    {
        private readonly ILogger<GetAllVirtualMachinesHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;

        public GetAllVirtualMachinesHandler(
            ILogger<GetAllVirtualMachinesHandler> logger, 
            IMediator mediator, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _mediator = mediator;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseDto<IEnumerable<VirtualMachineDto>>> Handle(
            GetAllVirtualMachinesQuery request, CancellationToken cancellationToken = default)
        {
            var response = new BaseResponseDto<IEnumerable<VirtualMachineDto>>();
            try
            {
                using (_unitOfWork)
                {
                    response.Data = (await _unitOfWork.VirtualMachines.GetAllAsync())
                        .Select(e => new VirtualMachineDto
                        {
                            Id = e.Id,
                            Name = e.Name
                        });
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