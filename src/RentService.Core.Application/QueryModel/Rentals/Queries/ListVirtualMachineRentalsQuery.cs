using System;
using System.Collections.Generic;
using RentService.Core.Application.QueryModel.VirtualMachines;

namespace RentService.Core.Application.QueryModel.Rentals.Queries
{
    public class ListVirtualMachineRentalsQuery : IQuery<IList<RentalQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public Guid VirtualMachineId { get; set; }
    }
}