using System;

namespace Infrastructure.Persistence.Entities
{
    public class VirtualMachineEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}