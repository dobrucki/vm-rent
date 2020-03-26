using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Dtos;
using Application.Core.Dtos.Requests;
using Application.Core.Models;
using Application.Core.Ports;
using MediatR;


namespace Application.Core.Services.VirtualMachineUseCases
{
    public class CreateVirtualMachineHandler : 
        IRequestHandler<CreateVirtualMachineRequest, BaseResponseDto<bool>>
    {
        private readonly IRepository<VirtualMachine> _repository;
        private readonly IMediator _mediator;

        public CreateVirtualMachineHandler(
            IRepository<VirtualMachine> repository,
            IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }
        
        public async Task<BaseResponseDto<bool>> Handle(
            CreateVirtualMachineRequest request, 
            CancellationToken cancellationToken)
        {
            var response = new BaseResponseDto<bool>();

            try
            {
                var virtualMachine = new VirtualMachine
                {
                    Name = request.Name
                };

                await _repository.CreateAsync(virtualMachine);
                response.Data = true;
            }
            catch (Exception ex)
            {
                response.Errors.Add("An error occurred while creating the virtual machine.");
            }

            return response;
        }
    }
}