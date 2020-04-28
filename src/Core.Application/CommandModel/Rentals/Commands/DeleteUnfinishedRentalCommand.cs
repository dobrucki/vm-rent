using System;

namespace Core.Application.CommandModel.Rentals.Commands
{
    public class DeleteUnfinishedRentalCommand : ICommand
    {
        public Guid Id { get; set; }
    }
}