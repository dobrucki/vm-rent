using System.Collections.Generic;
using Core.Domain.Rentals;
using Core.Domain.SharedKernel;

namespace Core.Domain.VirtualMachines
{
    public class VirtualMachine : ModelBase
    {
        public string Name { get; set; }
        public IList<Rental> Rentals { get; set; }
    }
}