using System;

namespace Core.Application.QueryModel.Rentals
{
    public sealed class RentalQueryEntity
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid VirtualMachineId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}