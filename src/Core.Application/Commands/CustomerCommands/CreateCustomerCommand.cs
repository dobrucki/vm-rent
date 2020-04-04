using Core.Application.Dtos;

namespace Core.Application.Commands.CustomerCommands
{
    public class CreateCustomerCommand : CommandBase<Result<CustomerDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}