using Core.Domain.Dtos;

namespace Core.Domain.Commands.CustomerCommands
{
    public class CreateCustomerCommand : CommandBase<BaseResponseDto<CustomerDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
    }
}