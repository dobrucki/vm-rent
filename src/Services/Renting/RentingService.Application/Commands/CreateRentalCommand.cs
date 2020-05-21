using System;

namespace RentingService.Application.Commands
{
    public class CreateRentalCommand : ICommand
    {
        public Guid Id { get; }
        public Guid CustomerId { get; }
        public string CustomerFirstName { get; }
        public string CustomerLastName { get; }
        public string CustomerEmailAddress { get; }
        public Guid VirtualMachineId { get; }
        public DateTime RentalTime { get; }

        public CreateRentalCommand(Guid id, Guid customerId, string customerFirstName, string customerLastName, 
            string customerEmailAddress, Guid virtualMachineId, DateTime rentalTime)
        {
            Id = id;
            CustomerId = customerId;
            CustomerFirstName = customerFirstName;
            CustomerLastName = customerLastName;
            CustomerEmailAddress = customerEmailAddress;
            VirtualMachineId = virtualMachineId;
            RentalTime = rentalTime;
        }
    }
}