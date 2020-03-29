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
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ListAllVirtualMachinesHandler> _logger;

        public ListAllVirtualMachinesHandler(
            ILogger<ListAllVirtualMachinesHandler> logger, 
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseDto<List<VirtualMachineDto>>> Handle(
            ListAllVirtualMachinesRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<List<VirtualMachineDto>>();

            try
            {
                await using (_unitOfWork)
                {
                    var virtualMachines = (await _unitOfWork.VirtualMachines.GetAllAsync())
                        .Select(e => new VirtualMachineDto
                        {
                            Name = e.Name,
                            Id = e.Id
                        }).ToList();
                    response.Data = virtualMachines;
                    await _unitOfWork.Complete();
                }
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