using System;

namespace RentService.Core.Application.CommandModel.Rentals.Commands
{
    public class DeleteUnfinishedRentalCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}