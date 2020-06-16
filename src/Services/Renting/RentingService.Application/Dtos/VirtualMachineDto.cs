using System;

namespace RentingService.Application.Dtos
{
    public class VirtualMachineDto
    {
        public Guid Id { get; }
        public string Name { get; }

        public VirtualMachineDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}