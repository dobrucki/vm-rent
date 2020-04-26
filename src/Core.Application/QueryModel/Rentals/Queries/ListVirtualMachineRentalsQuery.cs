using System;
using System.Collections.Generic;
using Core.Application.QueryModel.VirtualMachines;

namespace Core.Application.QueryModel.Rentals.Queries
{
    public class ListVirtualMachineRentalsQuery : IQuery<IList<RentalQueryEntity>>
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public Guid VirtualMachineId { get; set; }
    }
}