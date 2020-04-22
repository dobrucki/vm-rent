using System.Threading;
using System.Threading.Tasks;
using Core.Application.CommandModel.Customers.Commands;
using Core.Application.CommandModel.Customers.Events;
using Core.Domain.Customers;
using MediatR;

namespace Core.Application.CommandModel.Customers
{
    internal sealed class CustomerCommandHandler :
        ICommandHandler<CreateCustomerCommand>,
        ICommandHandler<EditCustomerDetailsCommand>
    {
        private readonly ICustomerRepository _customers;
        private readonly IMediator _mediator;

        public CustomerCommandHandler(ICustomerRepository customers, IMediator mediator)
        {
            _customers = customers;
            _mediator = mediator;
        }

        public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                EmailAddress = request.EmailAddress,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            await _customers.InsertOneAsync(customer);
            await _mediator.Publish(new CustomerCreatedEvent
            {
                Customer = customer
            }, cancellationToken);
            return Unit.Value;
        }

        public async Task<Unit> Handle(EditCustomerDetailsCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            await _customers.UpdateOneAsync(customer);
            return Unit.Value;
        }
    }
}