using System;
using System.Collections.Generic;

namespace Infrastructure.Persistence.Entities
{
    public class VirtualMachineEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<RentalEntity> Rentals { get; set; }
    }
}