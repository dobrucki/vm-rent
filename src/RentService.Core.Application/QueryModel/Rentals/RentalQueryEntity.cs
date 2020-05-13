using System;

namespace RentService.Core.Application.QueryModel.Rentals
{
    public sealed class RentalQueryEntity
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string VirtualMachineId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}